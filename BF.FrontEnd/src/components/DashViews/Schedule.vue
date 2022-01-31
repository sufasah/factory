<template>
  <v-container
    fill-height
    fluid
    grid-list-xl
  >
    <v-layout
      justify-center
      wrap
    >
      <v-flex
        md12
      >

        <crud-table
          title="Schedules"
          subTitle="List of schedules of workcenters"
          :headers="headers"
          :items="items"
          :editItem="editItem"
          :deleteItem="deleteItem"
          :add="add"
          :searchVal="searchVal"
          scheduleTable
        >
          <template slot="add-edit">
            <v-flex
              xs11
              mx-3
            >
              
              <v-btn
                color="general"
                style="width:100%;margin:0"
                @click="changeDateClick"
              >
                Change Dates
              </v-btn>
            </v-flex>
            <v-flex
              xs11
              mx-3
            >
              <v-text-field
                v-model="headers[2].editVal"
                label="Workcenter Id"
                type="number"
                readonly
                color="general"
                class="general-input"
              >
              </v-text-field>
            </v-flex> 
            <v-flex
              xs11
              mx-3
            >
              <v-text-field
                v-model="headers[10].editVal"
                label="Start Date"
                readonly
                color="general"
                class="general-input"
              >
              </v-text-field>
            </v-flex> 
            <v-flex
              xs11
              mx-3
            >
              <v-text-field
                v-model="headers[11].editVal"
                label="End Date"
                readonly
                color="general"
                class="general-input"
              >
              </v-text-field>
            </v-flex> 
          </template>
        </crud-table>

        <!--
          ----------------------------------------------------------- WORKCENTERS DIALOG
        -->
        <v-dialog
          v-model="changeDateDialog"
          max-width="1400px">
          <div style="width:100%;height:100%;background-color:#424242">
          <v-container
            fluid
            grid-list-xl
          >
            <v-text-field
                    class="mb-4 warning-input"
                    label="Search..."
                    hide-details
                    color="warning"
                    clearable
                    v-model="dialogSearch"
                    />

          
            <v-data-table
              :items="workcenterItems"
              :headers="workcenterHeaders"
              :search="dialogSearch"
            >
              <template
                slot="headerCell"
                slot-scope="{ header }"
              >
                <span
                  class="subheading font-weight-light text-general  text--darken-3"
                  v-text="header.text"
                />
              </template>
              <template
                slot="items"
                slot-scope="{ item }"
              >
                <td
                  :class="rowColorHandler(item)"
                >{{item.id}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.operationId}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.operationName}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.productType}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.workcenterId}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.workcenterName}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.workcenterActive}}</td>
                <td
                  :class="rowColorHandler(item)"
                >{{item.speed}}</td>
                <td
                  :class="rowColorHandler(item)"
                >
                  <div 
                    style="height:55px;"
                  >
                    <span
                      @click.capture="pickerClicked(item)"
                    >
                      <v-datetime-picker
                        v-model="item.picked"
                        label="Date"
                        color="general"
                        :datePickerProps="{
                          'min':new Date().toISOString().substring(0,10),
                          'allowed-dates':isAllowedDate
                        }"
                        :timePickerProps="{
                          'format':'24hr',
                          'allowed-hours':isAllowedHour,
                          'allowed-minutes':isAllowedMinute
                        }"
                        :formattedDatetimeChange="dateChanged"
                        :hourChange="hourChanged"
                        :checkChooseable="checkChooseable"
                        @input="(val)=>datePicked(val,item)"
                      >
                      </v-datetime-picker>
                    </span>
                    
                  </div>
                </td>
                <td
                >
                  <v-btn
                    icon
                    style="width:100%;margin:0"
                    @click="pickRow(item)"
                  >
                    <v-icon
                      color="green"
                      large
                    >
                      check
                    </v-icon>
                  </v-btn>
                </td>
              </template>
            </v-data-table>

            <div
              style="display:flex; flex-direction:row-reverse"
            >
              <v-btn
                text
                class="mt-4"
                color="green"
                :disabled="selectedItem == null"
                @click="changeDateDone()"
              >
                Done
              </v-btn>
              <v-btn
                text
                class="mt-4"
                @click="changeDateCancel()"
              >
                Cancel
              </v-btn>
            </div>
          </v-container>
          </div>
        </v-dialog>
      </v-flex>

<!-- 
  ------------------------------------------ SNACKBAR
-->

    <v-snackbar
        v-model="snackbar.show"
        :color="snackbar.color"
        :bottom="true"
      >
        {{ snackbar.msg }}
        <v-btn
          light
          flat
          @click="snackbar.show = false"
        >
          Close
        </v-btn>
    </v-snackbar>   
    </v-layout>
  </v-container>
</template>

<script>
import CrudTable from "./CrudTable"
export default {
  components:{"crud-table":CrudTable},
  props:{
    searchVal:{
      type:Object,
      default: ()=>{
        return {
          val:""
        }
      }
    }
  },
  data: () => ({
    headers: [
      {
        sortable:false,
      },
      {
        sortable: true,
        text: 'Id',
        value: 'id'
      },
      {
        sortable: true,
        text: 'Workcenter Id',
        value: 'workcenterId',
        myEditable:true,
        editVal:"",
        default:""
      },
      {
        sortable: true,
        text: 'Workcenter Name',
        value: 'workcenterName',
      },
      {
        sortable: true,
        text: 'Workcenter Active',
        value: 'workcenterActive',
      },
      {
        sortable: true,
        text: 'Product Id',
        value: 'productId',
      },
      {
        sortable: true,
        text: 'Product Name',
        value: 'productName'
      },
      {
        sortable: true,
        text: 'Product Type',
        value: 'productType',
        myEditable:true,
        editVal:"",
      },
      {
        sortable: true,
        text: 'Product Amount',
        value: 'productAmount'
      },
      {
        sortable: true,
        text: 'Product Salable',
        value: 'productSalable'
      },
      {
        sortable: true,
        text: 'Start',
        value: 'start',
        myEditable:true,
        editVal:"",
        defaultVal:"",
      },
      {
        sortable: true,
        text: 'End',
        value: 'end',
        myEditable:true,
        editVal:"",
        defaultVal:"",
      }
    ],
    items: [],
    workcenterHeaders: [
      {
        sortable: true,
        text: 'Id',
        value: 'id',
      },
      {
        sortable: true,
        text: 'Operation Id',
        value: 'operationId',
      },
      {
        sortable: true,
        text: 'Operation Name',
        value: 'operationName',
      },
      {
        sortable: true,
        text: 'Product Type',
        value: 'productType',
      },
      {
        sortable: true,
        text: 'Workcenter Id',
        value: 'workcenterId',
      },
      {
        sortable: true,
        text: 'Workcenter Name',
        value: 'workcenterName',
      },
      {
        sortable: true,
        text: 'Workcenter Active',
        value: 'workcenterActive',
      },
      {
        sortable: true,
        text: 'Speed',
        value: 'speed',
      },
      {
        sortable: true,
        text: 'Date Pick',
        value:'picked'
      },
      {
        sortable:false
      }
    ],
    workcenterItems: [],
    dialogSearch:"",
    changeDateDialog:false,
    selectedItem:null,
    notAllowedDates:new Map(),
    pickedDate:new Date(),
    selectedRowItem: null,
    pickedHour:0,
    snackbar:{
      show:false,
      color:"general",
      msg:"",
    }
  }),
  watch: {
  },
  mounted(){
    this.$store.dispatch("getSchedules")
    .then((result) => {
      this.items = result.data.data
    }).catch((err) => {
      console.log(err);
      console.log("Schedules could not be fetched");
    });

  },
  methods: {
    editItem (item,snackbar) {
      this.$store.dispatch("editSchedule",{id: item.id, start:this.headers[10].editVal, end:this.headers[11].editVal, speed:item.speed, workCenterId: item.workcenterId, productId: item.productId})
      .then((result) => {
        this.addEditItem(item)
        this.showSnackBar(snackbar,"general","Item Saved Successfully.")
      }).catch((err) => {
        this.snackbar.color = "red"
        this.snackbar.msg = "Server error occured. Check response from network. Item could not be saved."
        this.snackbar.show = true
      });
    },
    add (snackbar) {
      var item={}
      this.addEditItem(item)
      this.showSnackBar(snackbar,"green","Item Added Successfully.")
      this.items.push(item)
    },

    addEditItem(item){
      item.start = this.headers[10].editVal
      item.end = this.headers[11].editVal
      item.start = item.start.substring(0,item.start.length-1)
      item.end = item.end.substring(0,item.end.length-1)
    },

    showSnackBar(snackbar,color,msg){
      snackbar.show=true
      snackbar.color=color
      snackbar.msg=msg
    },
    deleteItem (item,snackbar) {
      this.$store.dispatch("deleteSchedule",item.id)
      .then((result) => {
        this.items.splice(this.items.indexOf(item),1)
        this.showSnackBar(snackbar,"red","Item Deleted Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be deleted.")
      });
    },
    changeDateClick(){

      this.$store.dispatch("getWorkcenterOperationsByType",this.headers[7].editVal)
        .then((result) => {
          this.operationSelectedCount = {}

          this.workcenterItems = result.data.data.map(e=>{
            return {
              id:e.id,
              operationId:e.operationId,
              operationName:e.operation.name,
              productType:e.operation.productType,
              workcenterId:e.workCenterId,
              workcenterName:e.workCenter.name,
              workcenterActive:e.workCenter.active,
              speed:e.speed,
              picked: null,
            }
          })
          this.notAllowedDates = new Map() 
          this.pickedDate = new Date()
          this.pickedHour = 0
          this.dialogSearch = ""
          this.selectedRowItem = null
          this.workcenterItems.forEach(e=>{
            e.picked = null
          })
          this.selectedItem = null
          this.changeDateDialog = true
        }).catch((err) => {
          this.snackbar.msg = "Server error eccured. Check response from network. Workcenter operations could not be fetched"
          this.snackbar.color = "red"
          this.snackbar.show = true
        });

    },
    pickRow(item){
      if(item.picked!=null){
        this.selectedItem=item
      }
    },
    rowColorHandler(item){
      if(this.selectedItem != null)
        return item.id == this.selectedItem.id ? 'green' : ''
      return ''
    },
    changeDateDone(){
      this.headers[10].editVal = new Date(this.selectedItem.picked.getTime()+1000*60*60*3).toISOString()
      this.headers[11].editVal = new Date(this.selectedItem.picked.getTime()+1000*60*(Math.floor(1000.0/this.selectedItem.speed))+1000*60*60*3).toISOString()
      this.changeDateDialog = false
    },
    changeDateCancel(){
      this.changeDateDialog = false
    },
    addNotAllowed(startDate,endDate,workcenterId){
      const newStart = startDate
      const newEnd = endDate
      const differentInMinutes = (newEnd.getTime()-newStart.getTime())/(1000*60)
      const datesWorkCenter = this.notAllowedDates.get(workcenterId)
      for(var i=0; i<= differentInMinutes; i++){
        var currentDate = new Date(newStart)
        currentDate.setMinutes(currentDate.getMinutes()+i)
        const key = new Date(currentDate.getTime()+1000*60*60*3).toISOString().substring(0,10)
        const dateMap = datesWorkCenter.get(key)
        if(!dateMap){
          dateMap = new Map()
          datesWorkCenter.set(key,dateMap)
        }
        const currentHour = currentDate.getHours()
        const hourSet = dateMap.get(currentHour)
        if(!hourSet){
          hourSet = new Set()
          dateMap.set(currentHour,hourSet)
        }
        
        const currentMinute = currentDate.getMinutes()
        if(!hourSet.has(currentMinute))
          hourSet.add(currentMinute)
      }
    },
    dateChanged(val){
      if(!!val){
        this.pickedDate = val
      }
    },
    hourChanged(val){
      if(!!val){
        this.pickedHour = Number(val)
      }
    },
    isAllowedDate(val){
      if(this.selectedRowItem == null)
        return true
      const datesWorkCenter = this.notAllowedDates.get(this.selectedRowItem.workcenterId)
      if(!datesWorkCenter)
        return true
      
      const dateMap = datesWorkCenter.get(val)
      if(!!dateMap){
        if(dateMap.size!=24)
          return true
        
        var allMinutesReserved=true
        for(var i of dateMap){
          if(i[1].size!=60){
            allMinutesReserved = false
            break
          }
        }

        return !allMinutesReserved
      }
      return true
    },
    isAllowedHour(val){
      if(this.selectedRowItem == null)
        return true
      const datesWorkCenter = this.notAllowedDates.get(this.selectedRowItem.workcenterId)
      if(!datesWorkCenter)
        return true

      const dateMap = datesWorkCenter.get(this.pickedDate)
      if(!!dateMap){
        const hourSet = dateMap.get(val)
        if(!hourSet)
          return true
        
        return hourSet.size != 60
        
      }
      return true
    },
    isAllowedMinute(val){
      if(this.selectedRowItem == null)
        return true
      const datesWorkCenter = this.notAllowedDates.get(this.selectedRowItem.workcenterId)
      if(!datesWorkCenter)
        return true

      const dateMap = datesWorkCenter.get(this.pickedDate)
      if(!!dateMap){
        const hourSet = dateMap.get(this.pickedHour)
        if(!hourSet)
          return true
        
        return !hourSet.has(val)
      }
      return true
    },
    isAllowedMinuteFull(val,date,hour){
      const datesWorkCenter = this.notAllowedDates.get(this.selectedRowItem.workcenterId)
      if(!datesWorkCenter)
        return true

      const dateMap = datesWorkCenter.get(date)
      if(!!dateMap){
        const hourSet = dateMap.get(hour)
        if(!hourSet)
          return true
        
        return !hourSet.has(val)
      }
      return true
    },
    datePicked(val,item){
      if(val == null)
        return
      
      if(!!this.selectedItem && item.id == this.selectedItem.id)
        this.selectedItem = null
    },
    pickerClicked(item){
      this.selectedRowItem = item
      this.$store.dispatch("getSchedulesByWorkcenterId",item.workcenterId)
      .then((result) => {
        var datesWorkCenter = this.notAllowedDates.get(item.workcenterId)
        if(!datesWorkCenter){
          datesWorkCenter = new Map()
          this.notAllowedDates.set(item.workcenterId,datesWorkCenter)

          for(var e of result.data.data){
            this.addNotAllowed(new Date(e.start),new Date(e.end),item.workcenterId)
          }
        }
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. Schedules of workcenter could not be fetched."
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
    },
    checkChooseable(startDate){
      const scheduleTime = Math.floor(1000.0/this.selectedRowItem.speed) 
      const endDate = new Date(startDate.getTime()+60000*scheduleTime)
      const differentInMinutes = (endDate.getTime()-startDate.getTime())/(1000*60)
      for(var i=0;i<=differentInMinutes;i++){
        var currentDate = new Date(startDate)
        currentDate.setMinutes(currentDate.getMinutes()+i)
        const date = new Date(currentDate.getTime()+1000*60*60*3).toISOString().substring(0,10)

        if(!this.isAllowedMinuteFull(currentDate.getMinutes(),date,currentDate.getHours()))
        {
          const firstSelectable = new Date(currentDate.getTime()-60000*(scheduleTime+1)+1000*60*60*3).toISOString()
          this.snackbar.color = "red"
          this.snackbar.msg = `Selected datetime is not chooseable. From this date to end of this production with this workcenter has confliction with another production. '${firstSelectable}' is first choosable time before selected datetime`
          this.snackbar.show = true
          return false
        }
      }
      return true
    }
  }
}
</script>

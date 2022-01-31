<template>
  <v-container
    fluid
    grid-list-xl
  >
    <v-layout 
      wrap
    >   
      <v-flex
        xs12
        md12
      >
        <material-card
          color="general"
          title="Product"
          text="To produce product and changing its properties."
        >
          <v-form>
            <v-container py-0>
              <v-layout wrap>
                
                <v-flex
                  xs12
                  md2
                >
                  <v-text-field
                    class="info-input"
                    label="Id"
                    v-model="product.id"
                    disabled
                  />
                </v-flex>
                <v-flex
                  xs12
                  md2
                >
                  <v-text-field
                    class="info-input"
                    label="Amount"
                    v-model="product.amount"
                    type="number"
                  />
                </v-flex>
                <v-flex
                  xs12
                  md4
                >
                  <v-text-field
                    label="Name"
                    v-model="product.name"
                    class="info-input"/>
                </v-flex>
                <v-flex
                  xs8
                  md3
                >
                  <v-text-field
                    label="Type"
                    v-model="product.type"
                    class="info-input"/>
                </v-flex>
                 <v-flex
                  xs4
                  md1
                >
                  <v-switch
                    label="Salable"
                    v-model="product.salable"
                    color="general"
                    :true-value="true"
                    :false-value="false"/>
                </v-flex>
                
                <v-flex
                  xs12
                  text-xs-right
                >
                  <v-btn
                    class="mx-1 font-weight-light"
                    color="general"
                    @click="updateProduct"
                  >
                    Update
                  </v-btn>
                  <v-btn
                    class="mx-1 font-weight-light"
                    color="red"
                    @click="deleteProduct"
                  >
                    Delete
                  </v-btn>
                </v-flex>
                
                
                <v-flex
                  xs6
                  md2
                >
                  <v-text-field
                    label="Amount to produce"
                    type="number"
                    v-model="produceAmount"
                    class="info-input"/>
                </v-flex>
                <v-flex
                  xs6
                  md2
                >
                  <v-btn
                    class="mt-3 font-weight-light"
                    color="green"
                    @click="produceClick"
                  >
                    Produce
                  </v-btn>
                </v-flex>
                <v-flex
                  xs12
                  style="margin-top:-20px"
                >
                  <v-data-table
                    :headers="headers"
                    :items="items"
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
                        :class="redRowHandler(item)"
                        @click="rowClickHandler(item.id)"
                        style="cursor:pointer"
                      >{{item.id}}</td>
                      <td 
                        :class="redRowHandler(item)"
                        @click="rowClickHandler(item.id)"
                        style="cursor:pointer"
                      >{{item.name}}</td>
                      <td 
                        :class="redRowHandler(item)"
                        @click="rowClickHandler(item.id)"
                        style="cursor:pointer"
                      >{{item.type}}</td>
                      <td 
                        :class="redRowHandler(item)"
                        @click="rowClickHandler(item.id)"
                        style="cursor:pointer"
                      >{{item.amount}}</td>
                      <td 
                        :class="redRowHandler(item)"
                        @click="rowClickHandler(item.id)"
                        style="cursor:pointer"
                      >{{item.neededAmount * produceAmount }}</td>
                    </template>
                  </v-data-table>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </material-card>
      </v-flex>

      <!--
        ----------------------------------------------------------- WORKCENTERS DIALOG
      -->
      <v-dialog
        v-model="produceDialog"
        max-width="1400px">
        <div style="width:100%;height:100%;background-color:#424242">

        <v-container  >
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
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.id}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.operationId}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.operationName}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.productType}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.workcenterId}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.workcenterName}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.workcenterActive}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.speed}}</td>
              <td
                @click="workcenterRowClickHandler(item)"
                style="cursor:pointer"
                :class="item.pickers.length > 0 ? 'green' : ''"
              >{{item.pickers.length}}</td>
            </template>
          </v-data-table>

          <div
            style="display:flex; flex-direction:row-reverse"
          >
            <v-btn
              text
              class="mt-4"
              color="green"
              @click="produceDone()"
              :disabled="produceDoneDisabled"
            >
              Done
            </v-btn>
            <v-btn
              text
              class="mt-4"
              @click="produceCancel()"
            >
              Cancel
            </v-btn>
          </div>
        </v-container>
        </div>
      </v-dialog>

      <!--
        ----------------------------------------------------------- SCHEDULE TIME DIALOG
      -->
      <v-dialog
        v-model="scheduleTimeDialog"
        max-width="600px"
      >
        <div id="for-dialog-dom" style="width:100%;height:100%;background-color:#424242">
        <v-container
          style="padding: 0; overflow:hidden;"
        >
          <v-layout
            wrap
          >
            <template
              v-for="(picker,i) in selectedRowItem == null ? [] :  selectedRowItem.pickers" 
            >
              <v-flex 
                xs11
                :style="`padding:0 24px;height:70px;background-color:${picker.val != null ? 'rgba(76,175,80,0.1)' : 'rgba(244,67,54,0.1)'}`"
              >
                <span
                  @click.capture="selectedPicker = picker"
                >
                  <v-datetime-picker 
                    v-model="picker.val"
                    :key="i"
                    :label="`${i+1}`"
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
                    @input="datePicked"
                  >
                  </v-datetime-picker>
                </span>
              </v-flex>
              <v-flex 
                xs1
                align-self-center
                :style="`cursor:pointer;height:70px;background-color:${picker.val != null ? 'rgba(76,175,80,0.1)' : 'rgba(244,67,54,0.1)'}`"
              >
                <v-btn
                  text
                  icon
                  class="mt-4"
                  color="lighten-2"
                  @click="minusClicked(i)"
                >
                  <v-icon
                    color="red"
                    x-large
                  >
                    mdi-minus
                  </v-icon>
                </v-btn>
              </v-flex>
            </template>
        
            <v-flex xs11 style="padding:0 24px;">
              <v-datetime-picker disabled >
              </v-datetime-picker>
            </v-flex>
            <v-flex 
              xs1
              align-self-center
              style="cursor:pointer"
            >
              <v-btn
                text
                icon
                color="lighten-2"
                @click="plusClicked"
              >
                <v-icon
                  color="green"
                  x-large
                >
                  mdi-plus
                </v-icon>
              </v-btn>
            </v-flex>
          </v-layout>
        </v-container>
        </div>
      </v-dialog>

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
export default {
  name: 'Product',
  data: () => ({
    headers: [
      {
        sortable: true,
        text: 'Id',
        value: 'id',
      },
      {
        sortable: true,
        text: 'Name',
        value: 'name'
      },
      {
        sortable: true,
        text: 'Type',
        value: 'type'
      },
      {
        sortable: true,
        text: 'Amount',
        value: 'amount'
      },
      {
        sortable: true,
        text: 'Needed Amount',
        value: 'neededAmount',
      },
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
        text: 'Assigned Product Amount',
        value:'pickers.length'
      },
    ],
    workcenterItems: [],
    product:{
      id:0,
      name:"",
      type:"",
      amount:0,
      salable:false,
    },
    dialogSearch:"",
    produceAmount:1,
    produceDialog:false,
    scheduleTimeDialog:false,
    selectedRowItem:null ,
    selectedPicker:null,
    beforeDatePickCount:null,
    isProducable:true,
    operationSelectedReachedProduceAmount: new Set(),
    operationSelectedCount:undefined,
    produceDoneDisabled:true,
    notAllowedDates:new Map(),
    pickedDate:new Date(),
    pickedHour:0,
    snackbar:{
      show:false,
      color:"general",
      msg:"",
    }
  }),
  created (){
  },
  watch: {
    '$route.params.id': function (id) {
      this.product.id = id

      this.$store.dispatch("getProduct",this.product.id)
      .then((result) => {
        const data = result.data.data
        if(data == null){
          this.$router.push("/panel/products-table")
          return
        }
        this.product.name = data.name
        this.product.type = data.type
        this.product.amount = data.amount
        this.product.salable = data.isSalable
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. This product could not be fetched."
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
      

      this.$store.dispatch("getSubProductOfProduct",this.product.id)
      .then((result) => {
        const data = result.data.data
        if(data == null)
          this.items = []
        else
          this.items = data.map(e=>{
            return {
              id:e.subProductId,
              name:e.subProductName,
              type:e.subProductType,
              amount:e.subProductAmount,
              neededAmount:e.amount
            }
          })
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. Sub products could not be fetched"
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
    },
    scheduleTimeDialog(active){
      if(!active){
        for(var i=0; i<this.selectedRowItem.pickers.length;i++){
          const e = this.selectedRowItem.pickers[i];
          if(e.val == null)
          {
            setTimeout(() => {
              this.scheduleTimeDialog = true
              this.snackbar.color = "red"
              this.snackbar.msg = "Fill all date fields or remove unused ones."
              this.snackbar.show = true
            }, 30);
            return
          }
        }

        this.operationSelectedCount[this.selectedRowItem.operationId] += this.selectedRowItem.pickers.length - this.beforeDatePickCount

        if(this.operationSelectedCount[this.selectedRowItem.operationId] == this.produceAmount)
          this.operationSelectedReachedProduceAmount.add(this.selectedRowItem.operationId)
        else
          this.operationSelectedReachedProduceAmount.delete(this.selectedRowItem.operationId)

        if(this.operationSelectedReachedProduceAmount.size != Object.keys(this.operationSelectedCount).length)
          this.produceDoneDisabled = true
        else
          this.produceDoneDisabled = false
        
      }
    },
    produceAmount(val){
      this.isProducable = true
    },

  },
  mounted () {
    this.diaEl = document.getElementById("for-dialog-dom").parentElement
    this.product.id = this.$route.params.id
    
    this.$store.dispatch("getProduct",this.product.id)
    .then((result) => {
      const data = result.data.data
      if(data == null){
        this.$router.push("/panel/products-table")
        return
      }
      this.product.name = data.name
      this.product.type = data.type
      this.product.amount = data.amount
      this.product.salable = data.isSalable
    }).catch((err) => {
      this.snackbar.msg = "Server error eccured. Check response from network. This product could not be fetched."
      this.snackbar.color = "red"
      this.snackbar.show = true
    });
    

    this.$store.dispatch("getSubProductOfProduct",this.product.id)
    .then((result) => {
      const data = result.data.data
      if(data == null)
        this.items = []
      else
        this.items = data.map(e=>{
          return {
            id:e.subProductId,
            name:e.subProductName,
            type:e.subProductType,
            amount:e.subProductAmount,
            neededAmount:e.amount
          }
        })
    }).catch((err) => {
      this.snackbar.msg = "Server error eccured. Check response from network. Sub products could not be fetched"
      this.snackbar.color = "red"
      this.snackbar.show = true
    });
  },
  methods: {
    redRowHandler(item){
      if(item.neededAmount * this.produceAmount > item.amount){
        this.isProducable = false
        return 'red'
      }
      else{
        return ''
      }
    },
    rowClickHandler(id){
      if(this.product.id!=id)
        this.$router.push(`/panel/products/${id}`)
    },
    workcenterRowClickHandler(item){
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

        this.selectedRowItem = item
        this.beforeDatePickCount = item.pickers.length
        this.scheduleTimeDialog=true
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. Schedules of workcenter could not be fetched."
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
    },
    produceClick(){
      if(!this.items || this.items.length==0 || !this.isProducable){
        this.snackbar.color = "warning"
        this.snackbar.msg = "There is no sub product or some of them does not exists for given produce amount."
        this.snackbar.show = true
        return
      }
      if(this.produceAmount < 1){
        this.snackbar.color = "red"
        this.snackbar.msg = "Produce amount must be greater than 0."
        this.snackbar.show = true
        return
      }
      this.$store.dispatch("getProduct",this.product.id)
      .then((result) => {
        const type = result.data.data.type
        this.$store.dispatch("getWorkcenterOperationsByType",type)
        .then((result) => {
          this.operationSelectedCount = {}

          this.workcenterItems = result.data.data.map(e=>{
            if(!(e.operationId in this.operationSelectedCount))
              this.operationSelectedCount[e.operationId]=0

            return {
              id:e.id,
              operationId:e.operationId,
              operationName:e.operation.name,
              productType:e.operation.productType,
              workcenterId:e.workCenterId,
              workcenterName:e.workCenter.name,
              workcenterActive:e.workCenter.active,
              speed:e.speed,
              pickers: []
            }
          })
          this.dialogSearch = ""
          this.selectedPicker = null
          this.notAllowedDates = new Map() 
          this.selectedRowItem = null 
          this.pickedDate = new Date()
          this.pickedHour = 0
          this.beforeDatePickCount = null
          this.operationSelectedReachedProduceAmount = new Set()
          this.produceDoneDisabled = true
          this.produceDialog = true
        }).catch((err) => {
          this.snackbar.msg = "Server error eccured. Check response from network. Workcenter operations could not be fetched"
          this.snackbar.color = "red"
          this.snackbar.show = true
        });
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. This product could not be fetched to get workcenter operations"
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
    },
    plusClicked(){
      const currentCount = this.operationSelectedCount[this.selectedRowItem.operationId] + this.selectedRowItem.pickers.length-this.beforeDatePickCount
        if(currentCount < this.produceAmount){

          this.selectedRowItem.pickers.push({
            val:null,
            beforeVal:null,
        })
        setTimeout(()=>{
          this.diaEl.scrollTo(0,9999999999999)
        },200)
      }
      else{
        this.snackbar.color="red"
        this.snackbar.msg="Reached produce amount that given at the beginning of production."
        this.snackbar.show = true;
      }
    },
    minusClicked(i){
      var picker = this.selectedRowItem.pickers[i]
      if(this.selectedPicker == picker)
        this.selectedPicker = null

      if(picker.val != null){
        this.removeNotAllowed(picker.val,new Date(picker.val.getTime()+60000*(Math.floor(1000.0/this.selectedRowItem.speed))))
      }
      
      this.selectedRowItem.pickers.splice(i,1)
    },
    produceDone(){
      var schedules = []
      this.workcenterItems.forEach(e => {
        for(var picker of e.pickers){
          schedules.push({
            productId: this.product.id,
            workcenterId: e.workcenterId,
            start: new Date(picker.val.getTime()+1000*60*60*3).toISOString(),
            end: new Date(picker.val.getTime()+60000*(Math.floor(1000.0/e.speed))+1000*60*60*3).toISOString()
          })
        }
      });
      this.$store.dispatch("addSchedules",schedules)
      .then((result) => {
        this.snackbar.color = "green"
        this.snackbar.msg = "Workcenters scheduled successfully."
        this.snackbar.show = true
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. Workcenters could not be scheduled."
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
      this.produceDialog = false
    },
    produceCancel(){
      this.produceDialog = false
    },
    updateProduct(){
      var temp = {...this.product}
      delete temp["salable"]
      temp.isSalable = this.product.salable
      this.$store.dispatch("editProduct",temp)
      .then((result) => {
        this.snackbar.color = "general"
        this.snackbar.msg = "Product successfully updated."
        this.snackbar.show = true
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. This product could not be updated."
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
    },
    deleteProduct(){
      this.$store.dispatch("deleteProduct",this.product.id)
      .then((result) => {
        this.snackbar.color = "red"
        this.snackbar.msg = "Product successfully deleted. Redirecting..."
        this.snackbar.show = true
        setTimeout(() => {
          this.$router.push("/panel/products-table")
        }, 2000);
      }).catch((err) => {
        this.snackbar.msg = "Server error eccured. Check response from network. This product could not be  deleted."
        this.snackbar.color = "red"
        this.snackbar.show = true
      });
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
    datePicked(val){
      const dateBefore = this.selectedPicker.beforeVal
      if(dateBefore != null){
        this.removeNotAllowed(dateBefore, new Date(dateBefore.getTime()+60000*(Math.floor(1000.0/this.selectedRowItem.speed))))
      }
      this.selectedPicker.beforeVal = val

      if(val == null)
        return

      this.addNotAllowed(val,new Date(val.getTime()+60000*(Math.floor(1000.0/this.selectedRowItem.speed))),this.selectedRowItem.workcenterId)

    },
    removeNotAllowed(startDate, endDate){

      const newStart = startDate
      const newEnd =  endDate
      const differentInMinutes = (newEnd.getTime()-newStart.getTime())/(1000*60)

      const datesWorkCenter = this.notAllowedDates.get(this.selectedRowItem.workcenterId)
      for(var i=0; i<= differentInMinutes; i++){
        var currentDate = new Date(newStart)
        currentDate.setMinutes(currentDate.getMinutes()+i)

        const key = new Date(currentDate.getTime()+1000*60*60*3).toISOString().substring(0,10)
        const dateMap = datesWorkCenter.get(key)
        
        const currentHour = currentDate.getHours()
        const hourSet = dateMap.get(currentHour)
        
        const currentMinute = currentDate.getMinutes()
        
        hourSet.delete(currentMinute)
        if(hourSet.size == 0){
          dateMap.delete(currentHour)
          if(dateMap.size == 0){
            datesWorkCenter.delete(key)
          }
        }
      }
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

<style scoped lang="css">

</style>
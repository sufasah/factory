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
          title="Workcenter Operations"
          subTitle="List of workcenter operations"
          :headers="headers"
          :items="items"
          :editItem="editItem"
          :deleteItem="deleteItem"
          :add="add"
          :searchVal="searchVal"
        >
          <template slot="add-edit">
            <v-flex
              xs11
              sm5
              mx-3
            >
              <v-text-field
                v-model="headers[2].editVal"
                label="Operation Id"
                type="number"
                color="general"
                class="general-input"
              >

              </v-text-field>
            </v-flex>
            <v-flex
              xs11
              sm5
              mx-3
            >
              <v-text-field
                v-model="headers[5].editVal"
                label="Workcenter Id"
                type="number"
                color="general"
                class="general-input"
              >

              </v-text-field>
            </v-flex>
            <v-flex
              xs11
              sm5
              mx-3
            >
              <v-text-field
                v-model="headers[8].editVal"
                label="Speed"
                type="number"
                color="general"
                class="general-input"
              >

              </v-text-field>
            </v-flex>
          </template>

        </crud-table>

      </v-flex>
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
    dialog: false,
    editedIndex: -1,
    editedItem: {
      nmae: '',
      country: '',
      city: '',
      isAdmin: true,
      isActive: true
    },
    headers: [
      {
        sortable:false,
      },
      {
        sortable: false,
        text: 'Id',
        value: 'id'
      },
      {
        sortable: false,
        text: 'Operation Id',
        value: 'operationId',
        myEditable:true,
        editVal:0,
        defaultVal:0,
      },
      {
        sortable: false,
        text: 'Operation Name',
        value: 'operationName',
      },
      {
        sortable: false,
        text: 'Operation Type',
        value: 'operationType',
      },
      {
        sortable: false,
        text: 'Workcenter Id',
        value: 'workcenterId',
        myEditable:true,
        editVal:0,
        defaultVal:0,
      },
      {
        sortable: false,
        text: 'Workcenter Name',
        value: 'workcenterName',
      },
      {
        sortable: false,
        text: 'Workcenter Active',
        value: 'workcenterActive',
      },
      {
        sortable: false,
        text: 'Speed',
        value: 'speed',
        myEditable:true,
        editVal:0,
        defaultVal:0,
      }
    ],
    items: []
  }),
  watch: {
  },
  mounted(){
    this.$store.dispatch("getWorkcenterOperations")
    .then((result) => {
      this.items = result.data.data.map(e=>{
        return {
          id:e.id,
          operationId:e.operationId,
          operationName:e.operation.name,
          operationType:e.operation.productType,
          workcenterId:e.workCenterId,
          workcenterName:e.workCenter.name,
          workcenterActive:e.workCenter.active,
          speed:e.speed
        }
      })
    }).catch((err) => {
      console.log(err);
      console.log("Workcenter operations could not be fetched");

    });
  },
  methods: {
    editItem (item,snackbar) {
      this.$store.dispatch("editWorkCenterOperation",{id:item.id,operationId:this.headers[2].editVal,workcenterId:this.headers[5].editVal,speed:this.headers[8].editVal})
      .then((result) => {
        var resData = result.data.data
        Object.keys(item).forEach(e => {item[e] = resData[e]});
        this.showSnackBar(snackbar,"general","Item Saved Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be saved.")
      });
    },
    add (snackbar) {
      var item={id:0}
      this.addEditItem(item)
      this.$store.dispatch("addWorkCenterOperation",item)
      .then((result) => {
        item = result.data.data
        this.items.push(item)
        this.showSnackBar(snackbar,"green","Item Added Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be added.")
      });
    },

    addEditItem(item){
      item.operationId = this.headers[2].editVal
      item.workcenterId = this.headers[5].editVal
      item.speed = this.headers[8].editVal
    },

    showSnackBar(snackbar,color,msg){
      snackbar.show=true
      snackbar.color=color
      snackbar.msg=msg
    },
    deleteItem (item,snackbar) {
      this.$store.dispatch("deleteWorkCenterOperation",item.id)
      .then((result) => {
        this.items.splice(this.items.indexOf(item),1)
        this.showSnackBar(snackbar,"red","Item Deleted Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be deleted.")
      });
    }
  }
}
</script>

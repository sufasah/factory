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
          title="Workcenters"
          subTitle="List of work centers"
          :headers="headers"
          :items="items"
          :editItem="editItem"
          :deleteItem="deleteItem"
          :add="add"
          :searchVal="searchVal"
        >
          <template slot="add-edit">
            <v-flex
              xs8
              sm5
              mx-3
            >
              <v-text-field
                v-model="headers[2].editVal"
                label="Name"
                color="general"
                class="general-input"
              >

              </v-text-field>
            </v-flex>
            <v-flex
              xs3
              mx-3
            >
              <v-switch
                  label="Active"
                  v-model="headers[3].editVal"
                  class="general-input"
                  color="general"
                  :true-value="true"
                  :false-value="false">

              </v-switch>
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
        sortable: true,
        text: 'Id',
        value: 'id',
      },
      {
        sortable: true,
        text: 'Name',
        value: 'name',
        myEditable:true,
        editVal:"",
        defaultVal:"",
      },
      {
        sortable: true,
        text: 'Active',
        value: 'active',
        myEditable:true,
        editVal:false,
        defaultVal:false,
      }
    ],
    items: []
  }),
  watch: {
  },
  mounted(){
    this.$store.dispatch("getWorkCenters")
    .then((result) => {
      this.items=result.data.data.map(e=>{return {id:e.id,name:e.name,active:e.active}})
    }).catch((err) => {
      console.log(err);
      console.log("Workcenters could not be fetched");

    });
  },
  methods: {
    editItem (item,snackbar) {
      this.$store.dispatch("editWorkCenter",{id:item.id,name:this.headers[2].editVal,active:this.headers[3].editVal})
      .then((result) => {
        this.addEditItem(item)
        this.showSnackBar(snackbar,"general","Item Saved Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be saved.")
      });
    },
    add (snackbar) {
      var item={id:0}
      this.addEditItem(item)
      this.$store.dispatch("addWorkCenter",item)
      .then((result) => {
        this.showSnackBar(snackbar,"green","Item Added Successfully.")
        this.items.push(item)
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be added.")
      });
    },

    addEditItem(item){
      item.name = this.headers[2].editVal
      item.active = this.headers[3].editVal
    },

    showSnackBar(snackbar,color,msg){
      snackbar.show=true
      snackbar.color=color
      snackbar.msg=msg
    },
    deleteItem (item,snackbar) {
      this.$store.dispatch("deleteWorkCenter",item.id)
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

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
          title="Products"
          subTitle="List of products"
          :headers="headers"
          :items="items"
          :editItem="editItem"
          :deleteItem="deleteItem"
          :add="add"
          clickableRow
          :onRowClick="onRowClick"
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
                label="Name"
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
                v-model="headers[3].editVal"
                label="Type"
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
                v-model="headers[4].editVal"
                label="Amount"
                type="number"
                min="0"
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
                  label="Salable"
                  v-model="headers[5].editVal"
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
        text: 'Type',
        value: 'type',
        myEditable:true,
        editVal:"",
        defaultVal:"",
      },
      {
        sortable: true,
        text: 'Amount',
        value: 'amount',
        myEditable:true,
        editVal:0,
        defaultVal:0,
      },
      {
        sortable: true,
        text: 'Salable',
        value: 'salable',
        myEditable:true,
        editVal:false,
        defaultVal:false,
      }
    ],
    items: []
  }),
  mounted(){
    this.$store.dispatch("getProducts")
    .then((result) => {
      this.items = result.data.data.map(e=>{
        return {
          id:e.id,
          name:e.name,
          type:e.type,
          amount:e.amount,
          salable:e.isSalable
        }
      })
    }).catch((err) => {
      console.log(err);
      console.log("Products could not be fetched");
    });
  },
  methods: {
    editItem (item,snackbar) {
      var temp = {...item}
      this.addEditItem(temp)
      temp.isSalable = temp.salable
      delete temp.salable
      this.$store.dispatch("editProduct",temp)
      .then((result) => {
        this.addEditItem(item)
        this.showSnackBar(snackbar,"general","Item Saved Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be saved.")
      });
    },

    add (snackbar) {
      var item = {}
      this.addEditItem(item)
      this.$store.dispatch("addProduct",item)
      .then((result) => {
        this.showSnackBar(snackbar,"green","Item Added Successfully.")
        this.items.push(result.data.data)
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be added.")
      });
    },

    addEditItem(item){
      item.name = this.headers[2].editVal
      item.type = this.headers[3].editVal
      item.amount = this.headers[4].editVal
      item.salable = this.headers[5].editVal
    },

    showSnackBar(snackbar,color,msg){
      snackbar.show=true
      snackbar.color=color
      snackbar.msg=msg
    },

    deleteItem (item,snackbar) {
      this.$store.dispatch("deleteProduct",item.id)
      .then((result) => {
        this.items.splice(this.items.indexOf(item),1)
        this.showSnackBar(snackbar,"red","Item Deleted Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be deleted.")
      });
    },

    onRowClick (item) {
      this.$router.push(`/panel/products/${item.id}`)
    }
  }
}
</script>

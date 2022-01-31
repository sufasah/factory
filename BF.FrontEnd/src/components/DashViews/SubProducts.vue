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
          title="Sub Products"
          subTitle="List of sub products as a tree shows which product made from"
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
                v-model="headers[1].editVal"
                label="Sub Product Id"
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
                v-model="headers[6].editVal"
                label="Product Id"
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
                v-model="headers[11].editVal"
                label="Needed Sub Product"
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
    headers: [
      {
        sortable:false,
      },
      {
        sortable: true,
        text: 'Sub Product Id',
        value: 'subProductId',
        myEditable:true,
        editVal:0,
        defaultVal:0,
      },
      {
        sortable: true,
        text: 'Sub Product Name',
        value: 'subProductName'
      },
      {
        sortable: true,
        text: 'Sub Product Type',
        value: 'subProductType'
      },
      {
        sortable: true,
        text: 'Sub Product Amount',
        value: 'subProductAmount'
      },
      {
        sortable: true,
        text: 'Sub Product Salable',
        value: 'subProductSalable'
      },
      {
        sortable: true,
        text: 'Product Id',
        value: 'productId',
        myEditable:true,
        editVal:0,
        defaultVal:0,
      },
      {
        sortable: true,
        text: 'Product Name',
        value: 'productName'
      },
      {
        sortable: true,
        text: 'Product Type',
        value: 'productType'
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
        text: 'Needed Sub Product',
        value: 'amount',
        myEditable:true,
        editVal:"",
        defaultVal:"",
      },
    ],
    items: []
  }),
  mounted(){
    this.$store.dispatch("getSubProducts")
    .then((result) => {
      this.items = result.data.data
    }).catch((err) => {
      console.log(err);
      console.log("Sub products could not be fetched");

    });
  },
  methods: {
    editItem (item,snackbar) {
      this.addEditItem(item)
      this.$store.dispatch("editSubProduct",item)
      .then((result) => {
        var resData = result.data.data
        Object.keys(item).forEach(e => {item[e] = resData[e]});
        this.showSnackBar(snackbar,"general","Item Saved Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be saved.")
      });
    },
    add (snackbar) {
      var item = {}
      this.addEditItem(item)
      this.$store.dispatch("addSubProduct",{id:item.subProductId,productId:item.productId,amount:item.amount})
      .then((result) => {
        item = result.data.data
        this.items.push(item)
        this.showSnackBar(snackbar,"green","Item Added Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be added.")
      });
    },

    addEditItem(item){
      item.subProductId = this.headers[1].editVal
      item.productId = this.headers[6].editVal
      item.amount = this.headers[11].editVal
    },

    showSnackBar(snackbar,color,msg){
      snackbar.show=true
      snackbar.color=color
      snackbar.msg=msg
    },
    deleteItem (item,snackbar) {
      this.$store.dispatch("deleteSubProduct",item.subProductId)
      .then((result) => {
        this.items.splice(this.items.indexOf(item),1)
        this.showSnackBar(snackbar,"red","Item Deleted Successfully.")
      }).catch((err) => {
        this.showSnackBar(snackbar,"red","Server error occured. Check response from network. Item could not be deleted.")
      });
    },
    onRowClick (item) {
      this.$router.push(`/panel/products/${item.productId}`)
    }
  }
}
</script>

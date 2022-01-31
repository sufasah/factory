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
          title="Orders"
          subTitle="List of customer orders"
          :headers="headers"
          :items="items"
          :editItem="editItem"
          orderTable
          :confirmOrder="confirmOrder"
          :cancelOrder="cancelOrder"
          :showOrderItems="showOrderItems"
          :searchVal="searchVal"
          :orderDialog="orderDialog"
        >
          <template slot="add-edit">
            <v-flex
              xs11
              mx-3
            >
              <v-select
                v-model="headers[6].editVal"
                :items="[{state:'Ordered',value:'Ordered'},{state:'OrderInQueue',value:'OrderInQueue'}]"
                item-text="state"
                item-value="value"
                label="State"
                return-object
                single-line
              ></v-select>
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
        text: 'Customer Id',
        value: 'customerId',
      },
      {
        sortable: true,
        text: 'Customer Name',
        value: 'customerName',
      },
      {
        sortable: true,
        text: 'Order Date',
        value: 'orderDate'
      },
      {
        sortable: true,
        text: 'Deadline',
        value: 'deadline'
      },
      {
        sortable: true,
        text: 'State',
        value: 'state',
        myEditable: true,
        editVal:"",
      },
      {
        sortable: true,
        text: 'Is Productable',
        value: 'isProductable',
      },
      {
        sortable:false
      }
    ],
    items: [],
    itemToOrderItems: new Map(),
    orderDialog:{
      show:false,
    }
  }),
  mounted () {
    this.items = []
    this.itemToOrderItems = new Map(),
    this.$store.dispatch("getOrdersPanel")
    .then((result) => {
      for(var i=0; i<result.data.data.length; i++){
        const e = result.data.data[i]
        var theItem = {
          id:e.id,
          customerId:e.customerId,
          customerName:e.customer.name,
          orderDate:e.orderDate,
          deadline:e.deadline,
          state:e.state,
        }
        
        var isProductable=true
        var theOrderItems = []
        
        e.orderItems.forEach(oi => {
          theOrderItems.push({
            id:oi.id,
            productId:oi.product.id,
            name:oi.product.name,
            type:oi.product.type,
            amount:oi.amount,
            amountAvailable:oi.product.amount,
            salable:oi.product.isSalable
          })
          
          if(oi.amount > oi.product.amount){
            isProductable = false
          }
        });
        theItem.isProductable = isProductable
        this.items.push(theItem)
        this.itemToOrderItems.set(theItem.id,theOrderItems)
      }
    }).catch((err) => {
      console.log(err);
      console.log("Orders could not be fetched");

    });
  },
  watch: {
    
  },
  methods: {
    editItem (item,snackbar) {
      const editVal = this.headers[6].editVal.state
      if(!(!!editVal) || editVal == item.state) return
      var temp = {
        ...item,
        'State': this.headers[6].editVal.value
      }
      this.$store.dispatch("editOrderPanel",temp)
      .then((result) => {
        item.state = editVal      
        snackbar.show=true
        snackbar.color="general"
        snackbar.msg="Item Saved Successfully."
      }).catch((err) => {
        snackbar.msg = "Server error eccured. Check response from network. Item could not be saved"
        snackbar.color = "red"
        snackbar.show = true
      });

    },
    confirmOrder(item,snackbar){
      if(!item.isProductable) {
        snackbar.color = "red"
        snackbar.msg = "This order has products that do not exists in stocks."
        snackbar.show = true
        return
      }
      this.$store.dispatch("confirmOrder",item.id)
      .then((result) => {
        item.state="OrderDelivered"
        snackbar.show=false
        snackbar.msg="Order Delivered Successfully."
        snackbar.color="green"
      setTimeout(()=>{
        snackbar.show=true
      },500)
      }).catch((err) => {
        snackbar.msg = "Server error eccured. Check response from network. Order could not be delivered"
        snackbar.color = "red"
        snackbar.show = true      });
    },
    cancelOrder(item,snackbar){
      var temp = {
        ...item,
        'State':"OrderCancelled",
      }
      this.$store.dispatch("editOrderPanel",temp)
      .then((result) => {
        item.state="OrderCancelled"
        snackbar.show=false
        snackbar.msg="Order Cancelled Successfully."
        snackbar.color="red"
      setTimeout(()=>{
        snackbar.show=true
      },500)
      }).catch((err) => {
        snackbar.msg = "Server error eccured. Check response from network. Order could not be cancelled"
        snackbar.color = "red"
        snackbar.show = true
      });
    },
    showOrderItems(item,assign){
      assign(this.itemToOrderItems.get(item.id))
      this.orderDialog.show=true
    }
  }
}
</script>

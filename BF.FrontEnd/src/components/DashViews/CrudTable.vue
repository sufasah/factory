<template>
  <material-card
    id="crud-table"
    color="general"
    flat
    full-width
    :title="title"
    :text="subTitle"
  >
    <div 
      style="display:flex;flex-direction:row-reverse"
      class="mb-4"
      v-if="!orderTable && !scheduleTable"
      >
      <v-btn
        color="green"
        dark
        style="margin:0!important;"
        @click="addEditClick({} ,true)"
        >Add</v-btn>
    </div>

    <v-data-table
      :headers="headers"
      :items="items"
      :search="searchVal.val"
      hide-actions
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
          v-if="!noAction"
          class="justify-center"
          :style="clickableRow ? 'cursor:pointer;':''"
          @click.self="onRowClick(item)"
        >
          <v-icon
            :style="isOrderEditable(item) ? '' : 'opacity:0;cursor:default'"
            medium
            color="general"
            class="mr-2"
            @click="addEditClick(item,false)">edit</v-icon>
          <v-icon
            v-if="!orderTable"
            medium
            color="red"
            @click="deleteItem(item,snackbar)">delete</v-icon>
            <v-icon
            v-if="orderTable && item.state!='OrderCancelled' && item.state!='OrderDelivered'"
            medium
            color="green"
            class="mr-2"
            @click="confirmOrder(item,snackbar)">check</v-icon>
          <v-icon
            v-if="orderTable && item.state!='OrderCancelled' && item.state!='OrderDelivered'"
            medium
            color="red"
            @click="cancelOrder(item,snackbar)">close</v-icon>
        </td>
        <template v-for="(value,key,index) in item">
          <td 
            :key="key"
            :class="isProductable(item)"
            :style="clickableRow ? 'cursor:pointer;':''"
            @click="onRowClick(item)"
          >
            {{ value }}
          </td>
        </template>
          <td
            v-if="orderTable"
            :class="isProductable(item)"
            :style="clickableRow ? 'cursor:pointer;':''"
            @click="onRowClick(item)"
          >
            <v-icon
            medium
            color="light-green"
            @click="showOrderItems(item,(x)=>orderItems = x)">list</v-icon>
          </td>
      </template>
    </v-data-table>

<!-- 
  ------------------------------------------ ADD-EDIT DIALOG
-->
    <v-dialog
      v-model="dialog"
      max-width="500px">
        <v-container>
          <v-layout
            wrap
          >
            <slot name="add-edit"></slot>
          </v-layout>
        </v-container>
        <div style="display:flex; flex-direction:row-reverse; margin:10px">
          <v-btn
            color="blue darken-1"
            elevation="2"
            outlined
            raised
            @click="close">Cancel</v-btn>
          <v-btn
            v-if="!editDialog"
            color="blue darken-1"
            elevation="2"
            outlined
            raised
            @click="add(snackbar),dialog=false">Add</v-btn>
          <v-btn
            v-if="editDialog"
            color="blue darken-1"
            elevation="2"
            outlined
            raised
            @click="editItem(dialogItem,snackbar);dialog=false">Edit</v-btn>
        </div>
    </v-dialog>

<!-- 
  ------------------------------------------ ORDER DIALOG
-->
    <v-dialog
      v-model="orderDialog.show"
      max-width="1300px">


      <v-container  >
        <v-text-field
                class="mb-4 warning-input"
                label="Search..."
                hide-details
                color="warning"
                clearable
                v-model="orderSearch"
                />

      
        <v-data-table
          :items="orderItems"
          :headers="orderItemHeaders"
          :search="orderSearch"
          hide-actions
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
            <template v-for="(value,key,index) in item">
              <td
                @click="productClicked(item.productId)"
                v-if="item.amount > item.amountAvailable"
                class="red"
                style="cursor:pointer"
                :key="key"
              >
                {{ value }}
              </td>
              <td
                style="cursor:pointer;"
                @click="productClicked(item.productId)"
                v-else
                :key="key"
              >
                {{ value }}
              </td>
            </template>
          </template>
        </v-data-table>
      </v-container>
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
  </material-card>
</template>

<script>
export default {
  name:"CrudTable",
  props:{
    title:{
      type:String,
      default:"Crud Table"
    },
    subTitle:{
      type:String,
      default:"Crud Table Subtitle"
    },
    headers:{
      type:Array,
      default:()=>[]
    },
    items:{
      type:Array,
      default:()=>[]
    },
    editValues:{
      type:Array,
      default:()=>[]
    },
    orderTable:{
      type:Boolean,
      default:false
    },
    scheduleTable:{
      type:Boolean,
      default:false
    },
    clickableRow:{
      type:Boolean,
      default:false
    },
    onRowClick:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    editItem:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    deleteItem:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    add:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    confirmOrder:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    cancelOrder:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    showOrderItems:{
      type:Function,
      default:function(){
        return () => {}
      }
    },
    orderDialog:{
      type:Object,
      default:() => {
        return {
          show:false

        }
      }
    },
    searchVal:{
      type:Object,
      default: ()=>{
        return {
          val:""
        }
      }
    },
    noAction:{
      type:Boolean,
      default: false
    }
  },
  data: () => ({
    orderItemHeaders:[
      {
        sortable: true,
        text: 'Id',
        value: 'id',
      },
      {
        sortable: true,
        text: 'Product Id',
        value: 'productId',
      },
      {
        sortable: true,
        text: 'Name',
        value: 'name',
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
        text: 'Amount Available',
        value: 'amountAvailable'
      },
      {
        sortable: true,
        text: 'Salable',
        value: 'salable'
      }
    ],
    
    dialog: false,
    editDialog: false,
    orderSearch:"",
    orderItems:[],
    dialogItem:{},
    snackbar:{
      show:false,
      color:"",
      msg:"",
    },
  }),
  watch:{
  },
  created(){
  },
  mounted(){
  },
  methods: {
    close () {
      this.dialog = false
    },
    productClicked (id) {
      this.$router.push(`/panel/products/${id}`)
    },
    isProductable(item){
      if(this.orderTable)
        return item.isProductable || (item.state!="Ordered" && item.state!="OrderInQueue") ?'':'red'
    },
    addEditClick(item,isAdd){
      this.dialogItem = item
      this.editDialog = !isAdd
      if(this.orderTable){
        if(!this.isOrderEditable(item))
          return
        this.headers[6].editVal = item.state
      }
      else{
        this.headers.forEach(e=>{
          if(e.myEditable)
            if(!isAdd)
              e.editVal = item[e.value]
            else
              e.editVal = e.defaultVal
        })
      }
        
      this.dialog=true;
    },
    isOrderEditable(item){
      return !this.orderTable || item.state=='Ordered' || item.state=='OrderInQueue'
    }
  }
}
</script>

<template lang="html">
  <v-app
    light
    >
    
  <v-navigation-drawer
    id="app-home-drawer"
    app
    light
    floating
    persistent
    mobile-break-point="0"
    width="260px"
  >
      <h3 class="text-xs-center mt-3 text-warning" style="background: #eaeaea;margin: 0!important; padding:17px!important">
      Cart
      </h3>
      <v-list-tile
        v-for="(cartItem,index) in cart" :key="cartItem.id"
        >
        <div>
          {{ cartItem.name}}
        </div>
        <v-spacer/>
        <div>
          {{ cartItem.amount}}
        </div>
        <v-spacer/>
        <v-btn
          class="cart-close"
          outlined
          fab
          color="danger"
          @click="deleteCartItem(index)"
        >
          <v-icon>mdi-close</v-icon>
        </v-btn>
      </v-list-tile>
      <hr>
      <v-form>
        <v-text-field
          v-model="deadline"
          style="margin:20px;margin-bottom:0"
          label="Deadline"
          :min="new Date().toISOString().substring(0,10)"
          type="date"
        >
        </v-text-field>
        <div class="d-flex justify-center">
          <v-btn
              outlined
              large
              color="green"
              @click="orderProducts()"
          >
              Order Products
          </v-btn>
        </div>
      </v-form>
  </v-navigation-drawer> 

  <v-container
    fill-height
    fluid
    grid-list-xl
    mt-5
  >
    <v-layout
      justify-center
      wrap
      mt-5
    >

      <core-toolbar
        :title="title"
        :searchVal="searchVal"
      />
  
      <v-flex
        md12
      >
        <material-card
          color="warning"
          title="Products"
          text="Here is some product available to buy"
        >
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
                class="subheading font-weight-light text-warning text--darken-3"
                v-text="header.text"
              />
            </template>
            <template
              slot="items"
              slot-scope="{ item }"
            >
              <td>{{ item.id }}</td>
              <td>{{ item.name }}</td>
              <td>{{ item.type }}</td>
              <td class="text-xs-right">
                
                <v-text-field
                  :ref="'amount-' + item.id"
                  :rules="[() => false]"
                  label="Amount"
                  align-center
                  justify-center
                  required
                  style="width:100px;display:inline-block;"
                  >
                </v-text-field>
                <v-btn
                  align-center
                  justify-center
                  color="green"
                  @click="addCart(item,'amount-'+item.id)">add cart
                </v-btn>
              </td>
            </template>
          </v-data-table>
        </material-card>
      </v-flex>
      <v-snackbar
        v-model="snackbar"
        :color="snackbarColor"
        :bottom="true"
      >
        {{ snackbarMessage }}
        <v-btn
          light
          flat
          @click="snackbar = false"
        >
          Close
        </v-btn>
      </v-snackbar>
    </v-layout>
  </v-container>
  </v-app>
</template>

<script lang="js">

import { mapMutations,mapState, mapGetters } from 'vuex'

export default {
  props:[],
  data: () => ({
    title: 'Product Shop',
     headers: [
      {
        sortable: true,
        text: "Id",
        value: "id"
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
        sortable: false,
        text: '',
        value: 'operations',
        align: 'right'
      }
    ],
    items: [],
    state: {},
    searchVal: {val:""},
    cart: [],
    snackbar: false,
    snackbarColor: "green",
    snackbarMessage: "Ordered Successfully.",
    deadline:"",
  }),

  computed: {
  },

  watch: {
    $route (val) {
      this.title = val.meta.name
    }
  },

  created () {
    this.state = this.$store.state
  },

  mounted () {
    this.$store.dispatch('getProducts',"isSalable=true")
    .then((result) => {
      this.items = result.data.data
    }).catch((err) => {
      this.snackbarColor = "red"
      this.snackbarMessage = "Server error occured. Check response from network. Products could not be fetched."
      this.snackbar = true
    });
  },

  methods: {
    
    addCart (item,refName) {;
      let amount = Number(this.$refs[refName].$refs["input"].value)

      if(!this.checkLogged()) return;

      if(amount < 1 )
        return;

      
      let findex;
      this.cart.forEach((e,index) => {
        if(e.id == item.id){
          findex=index;
          return
        }
      });


      if(findex !== undefined){
        this.cart.splice(findex,1,{
          ...this.cart[findex],
          amount:this.cart[findex].amount+amount
        })
      }
      else{
        item.amount = amount
        this.cart.push(item)
      }
      
    },

    deleteCartItem(index) {
      this.cart.splice(index,1)
    },

    orderProducts() {
      if(!this.checkLogged()) return;
      if(this.cart.length == 0){
        this.snackbarColor = "warning"
        this.snackbarMessage = "Select some products to order"
        this.snackbar = true
        return
      }
      if(!this.deadline){
        this.snackbarColor = "warning"
        this.snackbarMessage = "Select deadline to order"
        this.snackbar = true
        return
      }
      this.$store.dispatch('orderProducts',{cart:this.cart,deadline:this.deadline})
      .then((result) => {

        this.cart = []
        this.deadline = ""
        this.snackbarColor = "green"
        this.snackbarMessage = "Ordered successfully."
        this.snackbar = true
      }).catch((err) => {
        this.snackbarColor = "red"
        this.snackbarMessage = "Server error occured. Check response from network."
        this.snackbar = true
      });
    },

    checkLogged(){
      if(!this.state.customerLogged){
        this.$router.push("/home-login")
        return false
      }
      return true
    }
    

  }
}

</script>

<style lang="scss">
  #app-drawer {
    .v-list__tile {
      border-radius: 4px;

      &--buy {
        margin-top: auto;
        margin-bottom: 17px;
      }
    }

    .v-image__image--contain {
      top: 9px;
      height: 60%;
    }

    .search-input {
      margin-bottom: 30px !important;
      padding-left: 15px;
      padding-right: 15px;
    }
  }
  .cart-close{
    width:30px!important;
    height:30px!important;
  }
</style>

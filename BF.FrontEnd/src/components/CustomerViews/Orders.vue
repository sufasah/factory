<template lang="html">
  <v-app
    light
    >
    

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
        
      />
  
      <v-flex
        md12
      >
        <material-card
          color="warning"
          title="Orders"
          text="Order history"
        >
          <v-data-table
            :headers="headers"
            :items="items"
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
              <td>{{ item.deadline }}</td>
              <td>{{ item.orderDate }}</td>
              <td>{{ item.state }}</td>
              <td></td>
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
        text: 'Id',
        value: 'id'
      },
      {
        sortable: true,
        text: 'Deadline',
        value: 'deadline'
      },
      {
        sortable: true,
        text: 'Order Date',
        value: 'orderDate'
      },
      {
        sortable: true,
        text: 'State',
        value: 'state'
      }
    ],
    
    items: [],
    logged: false,
    searchVal: "",
    responsive: false,
    snackbar: false,
    snackbarColor: "green",
    snackbarMessage: "Ordered Successfully.",
  }),

  computed: {
  },

  watch: {
    $route (val) {
      this.title = val.meta.name
    }
  },

  created () {
    this.logged = this.$store.state.customerLogged
  },

  mounted () {
    this.$store.dispatch('getCustomerOrders')
    .then((result) => {
      this.items = result.data.data
    }).catch((err) => {
      this.snackbarColor = "red"
      this.snackbarMessage = "Server error occured. Check response from network. Orders could not be fetched."
      this.snackbar = true
    });
  },
  methods: {

    logout: function () {
      this.$store.dispatch('customerLogout').then(() => {
        this.logged = false
      })
    }
  }
}
</script>

<style lang="scss">
 
</style>

<template>
  <v-navigation-drawer
    id="app-drawer"
    app
    dark
    floating
    persistent
    width="260"
    mobile-break-point="0"
    absolute
  >
    <v-img
      :src="image"
      height="100%"
    >
      <v-layout
        class="fill-height"
        tag="v-list"
        column
      >
        <v-list-tile avatar>
          <v-list-tile-avatar
            color="grey"
          >
            <v-img
              :src="logo"
              height="64"
              contain
            />
          </v-list-tile-avatar>
          <v-list-tile-title class="title">
            User Panel
          </v-list-tile-title>
        </v-list-tile>
        <v-divider/>
    
        </v-list-tile>
        <v-list-tile
          v-for="(link, i) in links"
          :key="i"
          :to="link.to"
          :active-class="color"
          avatar
          class="v-list-item"
        >
          <v-list-tile-action>
            <v-icon>{{ link.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-title
            v-text="link.text"
          />
        </v-list-tile>
      </v-layout>
    </v-img>
  </v-navigation-drawer>
</template>

<script>
// Utilities
import {
  mapMutations,
  mapState
} from 'vuex'

export default {
  data: () => ({
    logo: require('@/assets/img/redditicon.png'),
    links: [
      {
        to: '/panel/dashboard',
        icon: 'mdi-view-dashboard',
        text: 'Dashboard'
      },
      {
        to: '/panel/user-profile',
        icon: 'mdi-account',
        text: 'User Profile'
      },
      {
        to:'/panel/orders',
        icon:'mdi-view-list',
        text:'Orders'
      },
      {
        to: '/panel/products-table',
        icon: 'mdi-wallet-giftcard',
        text: 'Products Table'
      },
      {
        to: '/panel/sub-products-table',
        icon: 'mdi-sitemap',
        text: 'Sub Products Table'
      },
      {
        to: '/panel/workcenters-table',
        icon: 'mdi-wrench',
        text: 'Workcenters Table'
      },
      {
        to: '/panel/operations-table',
        icon: 'mdi-vector-polyline',
        text: 'Operations Table'
      },
      {
        to: '/panel/workcenter-operations-table',
        icon: 'mdi-bulletin-board',
        text: 'Workcenter Opreations Table'
      },
      {
        to: '/panel/schedule-table',
        icon: 'mdi-calendar-clock',
        text: 'Schedule Table'
      }
    ]
  }),
  computed: {
    ...mapState('app', ['image', 'color']),
  },
  mounted () {
    
  },  
  methods: {

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
  }
</style>

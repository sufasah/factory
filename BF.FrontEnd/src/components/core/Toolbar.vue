<template>
  <v-toolbar
      id="core-toolbar"
      app
      :dark="dark"
      :light="!dark"
      flat
      prominent>
      <div class="v-toolbar-title">
        <v-toolbar-title class="font-weight-light text-warning">
          {{ title }}
        </v-toolbar-title>
      </div>
      <v-toolbar-items
        style="width:100%; justify-content:center;"
        >
        <v-flex
          align-center
          layout
          py-2
          :style="{flex:'0 1 auto', width: responsive ? '80%' : '50%'}"
            >
          <v-text-field
              class="mr-4 mt-2 warning-input"
              label="Search..."
              hide-details
              color="warning"
              clearable
              v-model="searchVal.val"
              />
        </v-flex>
      </v-toolbar-items>
          
      <v-toolbar-items>
        <v-flex
          align-center
          layout
          py-2>
        
          <router-link
            v-ripple
            class="toolbar-items"
            :to="panel ? '/panel/dashboard' : '/'">
            <v-icon color>mdi-home</v-icon>
          </router-link>
          
          <router-link
            v-if="!logged && !panel"
            v-ripple
            class="toolbar-items"
            to="/home-login">
            <v-icon color>mdi-account</v-icon>
          </router-link>

          <router-link
            v-if="logged && !panel"
            v-ripple
            class="toolbar-items"
            to="/orders">
            <v-icon color>mdi-cart</v-icon>
          </router-link>

          <v-icon
            v-if="panel || logged"
            class="toolbar-items"
            color
            @click="logout">mdi-power</v-icon>
        </v-flex>
      </v-toolbar-items>
    </v-toolbar>

</template>



<script lang="js">

  export default  {
    name: 'CoreToolbar',
    props: {
      title:{
        type:String,
        default:""
      },
      searchVal:{
        type:Object,
        default: function() {
          return {val:""}
        }
      },
      panel:{
        type:Boolean
      },
      dark:{
        type:Boolean
      }
    },
    data: () => ({
      logged: false,
      responsive: false
    }),
    mounted () {
      this.onResponsiveInverted()
      window.addEventListener('resize', this.onResponsiveInverted)
      if(this.panel)
        this.logged = this.$store.state.userLogged
      else
        this.logged = this.$store.state.customerLogged
    },
    beforeDestroy () {
      window.removeEventListener('resize', this.onResponsiveInverted)
    },
    created () {
    },
    methods: {
      logout: function () {
        if(this.panel){
          this.$store.dispatch('logout').then(()=>{
            this.logged = false
            this.$router.push("/login")
          })
        }
        else{
          this.$store.dispatch('customerLogout').then(() => {
            this.logged = false
          })
        }
      },
      onResponsiveInverted () { 
        if (window.innerWidth < 576) {
          this.responsive = true
        } else {
          this.responsive = false
        }
      },
    },
    computed: {

    }
}


</script>
<style>
#core-toolbar a {
text-decoration: none;
}
</style>

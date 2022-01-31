<template>
  <v-app
    id="dashboard-view"
    dark>
    <core-filter />

    <core-toolbar 
      panel
      dark
      :searchVal="searchVal"
    />

    <core-drawer />

    <v-content>
      <v-fade-transition mode="out-in">
        <router-view
          :searchVal="searchVal"
        />

      </v-fade-transition>
    </v-content>

    <core-footer v-if="$route.meta.name !== 'Maps'" />

  </v-app>
</template>

<script>
export default {
  name: 'DashboardView',
  data: () => ({
    searchVal:{
      val:""
    }
  }),
  metaInfo () {
    return {
      title: 'My Factory Panel'
    }
  }
}
</script>

<style>

</style>

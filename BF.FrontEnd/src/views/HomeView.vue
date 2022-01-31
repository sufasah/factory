<template>
  <v-app
    id="home"
    light>

    <v-content>
      <transition mode="out-in">
        <router-view>
        </router-view>
      </transition>
    </v-content>

  </v-app>
</template>

<script>
export default {
}
</script>

<style>
</style>

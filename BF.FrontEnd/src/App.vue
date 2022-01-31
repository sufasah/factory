<template>
  <transition mode="out-in">
    <router-view />
  </transition>
</template>

<script>
export default {
 
  created: function () {
  }
}
</script>

<style lang="scss">
$nprogress-color: #389df1;
$nprogress-height: 4px;

@import "nprogress/css/nprogress.scss";
@import "@/styles/index.scss";

/* Remove in 1.2 */
.v-datatable thead th.column.sortable i {
  vertical-align: unset;
}
</style>
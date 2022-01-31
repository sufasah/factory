// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'

// Components
import './components'

// Plugins
import './plugins'

// Sync router with store
import { sync } from 'vuex-router-sync'

// Application imports
import App from './App'
import router from '@/router'
import store from '@/store'
import Vuetify from 'vuetify'
import DatetimePicker from '@/components/DatetimePicker'
import theme from '@/plugins/theme'
import 'vuetify/dist/vuetify.min.css'
import '@mdi/font/css/materialdesignicons.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import axios from 'axios'
import NProgress from 'nprogress'

Vue.prototype.$http = axios

axios.defaults.baseURL = 'https://localhost:5001/api/v1'
axios.defaults.headers.get['Accept'] = 'application/json'


axios.interceptors.request.use(
  function (request) {
    NProgress.start()
    return request
  },
  function (error) {
    NProgress.done()
    return Promise.reject(error)
  }
)

// Add a response interceptor
axios.interceptors.response.use(
  function (response) {
    NProgress.done()
    return response
  },
  function (error) {
    console.log(error)
    NProgress.done()
    return Promise.reject(error)
  }
)

// Sync store with router
sync(store, router)

const config = {
  aria: true,
  classNames: {},
  classes: false,
  delay: 0,
  dictionary: null,
  errorBagName: 'errors', // change if property conflicts
  events: 'input|blur',
  fieldsBagName: 'fields',
  inject: true,
  locale: 'en',
  validity: false,
  useConstraintAttrs: true
};

Vue.use(Vue => {
  Vue.component('v-datetime-picker', DatetimePicker)
})

Vue.use(
  Vuetify,{
  iconfont: 'mdi',
  theme
  }
)

Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

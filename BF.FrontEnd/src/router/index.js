
/**
 * Vue Router
 *
 * @library
 *
 * https://router.vuejs.org/en/
 */

// Lib imports
import Vue from 'vue'
import Router from 'vue-router'
import store from '../store'
import Meta from 'vue-meta'
import NProgress from 'nprogress'

// Routes
import paths from './paths'

Vue.use(Router)

// Create a new router
const router = new Router({
  base: '/',
  mode: 'history',
  routes: paths,

  scrollBehavior (to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    }
    if (to.hash) {
      return { selector: to.hash }
    }
    return { x: 0, y: 0 }
  }
})

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.authorized) {
      next()
      return
    }
    next('/login')
  } else {
    next()
  }
})

router.beforeResolve((to, from, next) => {
  if (to.path) {
    NProgress.start()
  }
  next()
})

router.afterEach(() => {
  NProgress.done()
})

Vue.use(Meta)

export default router

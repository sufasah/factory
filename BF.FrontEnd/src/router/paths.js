import store from '../store'
export default [
  {
    path: '*',
    meta: {
      name: '',
      requiresAuth: true
    },
    redirect: {
      path: '/'
    }
  },
  {
    path: '/',
    meta: {
      name: '',
      requiresAuth: false
    },
    component: () => import('@/views/LoginHome.vue'),
    children: [
      {
        path: '',
        component: () => import('@/components/CustomerViews/Home.vue')
      },
      {
        path: 'home-login',
        component: () => import('@/components/CustomerViews/Login.vue'),
        beforeEnter: (to, from, next) => {
          if (store.getters.customerAuthorized) {
            next("")
          } else {
            next()
          }
        }
      },
      {
        path: 'orders',
        component: () => import('@/components/CustomerViews/Orders.vue')
      }
    ]
  },

  {
    path: '/login',
    meta: {
      name: '',
      requiresAuth: false
    },
    component: () =>
      import(`@/views/LoginHome.vue`),
    beforeEnter: (to, from, next) => {
      if (store.getters.authorized) {
        next('/panel/dashboard')
      } else {
        next()
      }
    },
    children: [
      {
        path: '',
        component: () => import(`@/components/LoginForm.vue`)
      }
    ]
  },

  {
    path: '/panel',
    meta: {
      name: 'Dashboard View',
      requiresAuth: true
    },
    component: () => import(`@/views/DashboardView.vue`),
    children: [
      {
        path: '',
        beforeEnter:(to,from,next)=>{
          next("/panel/dashboard")
        }
      },
      {
        path: 'dashboard',
        name: 'Dashboard',
        component: () => import(`@/components/DashViews/Dashboard.vue`)
      },
      {
        path: 'user-profile',
        meta: {
          name: 'User Profile',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/UserProfile.vue`)
      },
      {
        path: 'orders',
        meta: {
          name: 'Order Tables',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/Orders.vue`)
      },
      {
        path: 'products-table',
        meta: {
          name: 'Product Table',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/Products.vue`)
      },
      {
        path: 'sub-products-table',
        meta: {
          name: 'Sub Product Table',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/SubProducts.vue`)
      },
      {
        path: 'workcenters-table',
        meta: {
          name: 'Workcenter Table',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/Workcenters.vue`)
      },
      {
        path: 'operations-table',
        meta: {
          name: 'Operation Table',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/Operations.vue`)
      },
      {
        path: 'workcenter-operations-table',
        meta: {
          name: 'Workcenter Operations Table',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/WorkcenterOperations.vue`)
      },
      {
        path: 'schedule-table',
        meta: {
          name: 'Schedule Table',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/Schedule.vue`)
      },
      {
        path: 'products/:id',
        meta: {
          name: 'Product',
          requiresAuth: true
        },
        component: () => import(`@/components/DashViews/Product.vue`)
      }
    ]
  }
]

// https://vuex.vuejs.org/en/actions.html
import axios from 'axios'

// The login action passes vuex commit helper that we will use to trigger mutations.
export default {
  login ({ commit }, userData) {
    return new Promise((resolve, reject) => {
      commit('auth_request')
      axios.post('/users/login', { name: userData.username, password: userData.password })
      .then(response => {
          let logged = response.data.data.logged
          let username = userData.username
          let userId = response.data.data.id

          localStorage.setItem('myfactory_user_logged', logged)
          localStorage.setItem('myfactory_user', username)
          localStorage.setItem('myfactory_user_id',userId)

          commit('auth_success', { userId, logged })
          resolve(response)
        })
        .catch(err => {
          commit('auth_error')
          localStorage.removeItem('myfactory_user_logged')
          localStorage.removeItem('myfactory_user')
          localStorage.removeItem('myfactory_user_id')
          reject(err)
        })
    })
  },
  logout ({ commit }) {
    return new Promise((resolve, reject) => {
      commit('logout')
      localStorage.removeItem('myfactory_user_logged')
      localStorage.removeItem('myfactory_user')
      localStorage.removeItem('myfactory_user_id')
      resolve()
    })
  },

  customerLogin ({ commit }, customerData) {
    return new Promise((resolve, reject) => {
      commit('customer_auth_request')
      axios.post('/customers/login', { name: customerData.username, password: customerData.password })
      .then(response => {
          let logged = response.data.data.logged
          let username = customerData.username
          let customerId = response.data.data.id

          localStorage.setItem('myfactory_customer_logged', logged)
          localStorage.setItem('myfactory_customer', username)
          localStorage.setItem('myfactory_customer_id',customerId)
          
          commit('customer_auth_success', { customerId, logged })
          resolve(response)
        })
        .catch(err => {
          commit('customer_auth_error')
          localStorage.removeItem('myfactory_customer_logged')
          localStorage.removeItem('myfactory_customer')
          localStorage.removeItem('myfactory_customer_id')
          reject(err)
        })
    })
  },
  customerLogout ({ commit }) {
    return new Promise((resolve, reject) => {
      commit('customerLogout')
      localStorage.removeItem('myfactory_customer_logged')
      localStorage.removeItem('myfactory_customer')
      localStorage.removeItem('myfactory_customer_id')
      resolve()
    })
  },

  customerRegister ({ commit }, customerData) {
    return new Promise((resolve, reject) => {
      commit('customer_auth_request')
      axios.post('/customers', { name: customerData.username, password: customerData.password })
      .then(response => {
        resolve(response)
      })
      .catch(err => {
        reject(err)
      })
    })
  },
  getOrdersGroupedByState ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get(`/orders/dashboard`).then(response=>{
        var res = {}
        response.data.data.forEach(e => {
          if(e.state == "Ordered")
            res.ordered = e.count
          else if (e.state == "OrderInQueue")
            res.orderInQueue = e.count
          else if(e.state == "OrderDelivered")
            res.orderDelivered = e.count
          else if(e.state == "OrderCancelled")
            res.orderCancelled = e.count
        });

        if(!res.ordered)
          res.ordered = 0
        if(!res.orderInQueue)
          res.orderInQueue = 0
        if(!res.orderDelivered)
          res.orderDelivered = 0
        if(!res.orderCancelled)
          res. orderCancelled = 0

        response.data.data = res

        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getWorkcentersGroupedByActive ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get(`/workcenters/dashboard`).then(response=>{
        var res = {}
        response.data.data.forEach(e => {
          if(e.active == true)
            res.active = e.count
          else if (e.active == false)
            res.notActive = e.count
        });

        if(!res.active)
          res.active = 0
        if(!res.notActive)
          res.notActive = 0

        response.data.data = res

        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getCount({ commit }, {table,func}){
    return new Promise((resolve,reject)=>{
      axios.get(`/${table}/${func}`).then((result) => {
        resolve(result)
      }).catch((err) => {
        reject(err)
      });
    })
  },
  getSubProductOfProduct ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.get(`/subproducts?ProductId=${id}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },

  orderProducts ({ commit, state }, {cart, deadline}) {
    return new Promise((resolve, reject) => {
      const order={}
      order.customerId = state.customerId
      order.deadline = deadline
      order.orderItems = cart.map(v=>{
        return {
          productId:v.id,
          amount:v.amount,
        }
      })
      order.orderDate = new Date(Date.now()+1000*60*60*3).toISOString()
      order.state = "Ordered"
      axios.post('/orders',order).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },

  getCustomerOrders ({ commit, state }) {
    return new Promise((resolve, reject) => {
      const customerId = state.customerId
      
      axios.get(`/orders?CustomerId=${customerId}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  updateUser ({ commit },{username,password}) {
    return new Promise((resolve, reject) => {
      const userId = localStorage.getItem('myfactory_user_id')
      axios.put(`/users/${userId}`,{id:userId, name: username, password: password}).then(response=>{
        localStorage.setItem('myfactory_user', username)
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getOrdersPanel ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/orders/panel').then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editOrderPanel ({ commit },order) {
    return new Promise((resolve, reject) => {
      axios.put(`/orders/${order.id}`,order).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  confirmOrder ({ commit },id) {
    return new Promise((resolve, reject) => {
      axios.put(`/orders/${id}?confirm=true`,{id:id}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editProduct ({ commit },product) {
    return new Promise((resolve, reject) => {
      axios.put(`/products/${product.id}`,product).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  addProduct ({ commit },item) {
    return new Promise((resolve, reject) => {
      item.isSalable = item.salable
      delete item.salable
      axios.post('/products',item).then(response=>{
        const data = response.data.data
        response.data.data={
          id:data.id,
          name:data.name,
          type:data.type,
          amount:data.amount,
          salable:data.isSalable
        }
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  deleteProduct ({ commit },productId) {
    return new Promise((resolve, reject) => {
      axios.delete(`/products/${productId}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getProducts ({ commit }, query) {
    return new Promise((resolve, reject) => {
      axios.get(`/products?${query}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getProduct ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.get(`/products/${id}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getSubProducts ({ commit },) {
    return new Promise((resolve, reject) => {
      axios.get('/subproducts').then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  deleteSubProduct ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.delete(`/subproducts/${id}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  addSubProduct ({ commit }, subProduct) {
    return new Promise((resolve, reject) => {
      axios.post('/subproducts',subProduct).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editSubProduct ({ commit } , subProduct) {
    return new Promise((resolve, reject) => {
      axios.put(`/subproducts/${subProduct.subProductId}`,{id: subProduct.subProductId,productId: subProduct.productId, amount: subProduct.amount}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getWorkCenters ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/workcenters').then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  
  addWorkCenter ({ commit },workCenter) {
    return new Promise((resolve, reject) => {
      axios.post('/workcenters',workCenter).then(response=>{
        workCenter.id=response.data.data.id
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editWorkCenter ({ commit }, workCenter) {
    return new Promise((resolve, reject) => {
      axios.put(`/workcenters/${workCenter.id}`,workCenter).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },

  deleteWorkCenter ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.delete(`/workcenters/${id}`,{id:id}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getOperations ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/operations').then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  addOperation ({ commit },operation) {
    return new Promise((resolve, reject) => {
      axios.post('/operations',operation).then(response=>{
        operation.id=response.data.data.id
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editOperation ({ commit }, operation) {
    return new Promise((resolve, reject) => {
      axios.put(`/operations/${operation.id}`,operation).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },

  deleteOperation ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.delete(`/operations/${id}`,{id:id}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  addWorkCenterOperation ({ commit },workCenterOperation) {
    return new Promise((resolve, reject) => {
      axios.post('/workcenteroperations',{...workCenterOperation, workCenterId:workCenterOperation.workcenterId}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editWorkCenterOperation ({ commit }, workCenterOperation) {
    return new Promise((resolve, reject) => {
      axios.put(`/workcenteroperations/${workCenterOperation.id}`,{id:workCenterOperation.id,operationId:workCenterOperation.operationId,workCenterId:workCenterOperation.workcenterId, speed:workCenterOperation.speed}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  
  getWorkcenterOperations ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/workcenteroperations').then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getWorkcenterOperationsByType ({ commit }, type) {
    return new Promise((resolve, reject) => {
      axios.get(`/workcenteroperations?type=${type}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },

  deleteWorkCenterOperation ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.delete(`/workcenteroperations/${id}`,{id:id}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  addSchedule ({ commit },schedule) {
    return new Promise((resolve, reject) => {
      axios.post('/schedules',{...schedule, workCenterId:schedule.workcenterId}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  addSchedules ({ commit },schedules) {
    return new Promise((resolve, reject) => {
      axios.post('/schedules/list',schedules).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  editSchedule ({ commit }, schedule) {
    return new Promise((resolve, reject) => {
      axios.put(`/schedules/${schedule.id}`,schedule).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  
  getSchedules ({ commit }) {
    return new Promise((resolve, reject) => {
      axios.get('/schedules').then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  getSchedulesByWorkcenterId ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.get(`/schedules?workcenterId=${id}`).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },

  deleteSchedule ({ commit }, id) {
    return new Promise((resolve, reject) => {
      axios.delete(`/schedules/${id}`,{id:id}).then(response=>{
        resolve(response)
      })
      .catch(err=>{
        reject(err)
      })
    })
  },
  
}

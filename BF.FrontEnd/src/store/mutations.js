// https://vuex.vuejs.org/en/mutations.html

export default {
  auth_request (state) {
    state.authStatus = 'loading'
  },
  customer_auth_request(state) {
    state.customerAuthStatus = 'loading'
  },
  auth_success (state, { userId, logged }) {
    state.authStatus = 'success'
    state.logged = logged
    state.userId = userId
  },
  customer_auth_success (state, { customerId, logged }) {
    state.customerAuthStatus = 'success'
    state.customerLogged = logged
    state.customerId = customerId
  },
  user_auth_error (state) {
    state.authStatus = 'error'
  },
  customer_auth_error (state) {
    state.customerAuthStatus = 'error'
  },
  logout (state) {
    state.authStatus = ''
    state.logged = false
  },
  customerLogout(state){
    state.customerAuthStatus=''
    state.customerLogged = false
  },
  setTableList (state, tableList) {
    state.tableList = tableList
  }
}

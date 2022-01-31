// https://vuex.vuejs.org/en/state.html

export default {
  authStatus: '',
  customerAuthStatus: '',
  logged: localStorage.getItem('myfactory_user_logged') == "true",
  customerLogged: localStorage.getItem('myfactory_customer_logged') == "true",
  userId: localStorage.getItem('myfactory_user_id'),
  customerId: localStorage.getItem('myfactory_customer_id'),
  tableList: []
}

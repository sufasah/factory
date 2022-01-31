// https://vuex.vuejs.org/en/getters.html

// authorized lets you know if the token is true or not
export default {
  authorized: state => state.logged || false,
  authstatus: state => state.authStatus,
  customerAuthorized: state => {
    state.customerLogged || false},
  customerAuthstatus: state => state.customerAuthStatus,
}

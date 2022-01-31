<template>
  <v-content>
    <v-container
      fill-height
      fluid>
      <v-layout
        align-center
        justify-center>
        <v-flex
          xs12
          sm8
          md4>
          <v-card
            class="elevation-12">
            <v-toolbar
              color="warning">
              <v-toolbar-title>Customer Login</v-toolbar-title>
              <v-spacer/>
            </v-toolbar>
            <v-card-text>
              <v-form ref="form">
                <v-text-field
                  ref="username"
                  v-model="username"
                  :rules="usernameRules"
                  prepend-icon="mdi-account"
                  label="Username"
                  placeholder="yuzirneym"
                  required
                />
                <v-text-field
                  ref="password"
                  v-model="password"
                  :rules="passwordRules"
                  :append-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
                  :type="showPassword ? 'text' : 'password'"
                  prepend-icon="mdi-lock"
                  label="Password"
                  placeholder="*********"
                  counter
                  required
                  @keydown.enter="login"
                  @click:append="showPassword = !showPassword"
                />
                <v-text-field
                  v-if="registerForm"
                  ref="passwordAgain"
                  v-model="passwordAgain"
                  :rules="passwordAgainRules"
                  :append-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
                  :type="showPassword ? 'text' : 'password'"
                  prepend-icon="mdi-lock"
                  label="Password"
                  placeholder="*********"
                  counter
                  required
                  @keydown.enter="register"
                  @click:append="showPassword = !showPassword"
                />
              </v-form>
            </v-card-text>
            <v-divider class="mt-5"/>
            <v-card-actions>
              <v-btn
                v-if="!registerForm"
                align-center
                justify-center
                color="warning"
                @click="setRegisterForm(true)">Register
              </v-btn>
              <v-btn
                v-if="registerForm"
                align-center
                justify-center
                color="warning"
                @click="setRegisterForm(false)">Login
              </v-btn>
              <v-spacer/>
              <v-btn
                v-if="!registerForm"
                align-center
                justify-center
                color="warning"
                @click="login">Login
              </v-btn>
              <v-btn
                v-if="registerForm"
                align-center
                justify-center
                color="warning"
                @click="register">Register
              </v-btn>
            </v-card-actions>
            <v-snackbar
              v-model="snackbar.show"
              :color="snackbar.color"
              :bottom="true"
            >
              {{snackbar.msg}}
              <v-btn
                light
                flat
                @click="snackbar.show = false"
              >
                Close
              </v-btn>
            </v-snackbar>
          </v-card>
        </v-flex>
      </v-layout>

    </v-container>
  </v-content>
</template>

<script>
export default {
  data: function () {
    return {
      username: '',
      password: '',
      passwordAgain: '',
      snackbar: {show:false,color:"",msg:""},
      showPassword: false,
      registerForm: false,
      usernameRules: [() => !!this.username || 'This field is required'],
      passwordRules : [() => !!this.password || 'This field is required'],
      passwordAgainRules : [() => !!this.passwordAgain || 'This field is required'],
      passwordMatchRule: () => this.password!=this.passwordAgain ? 'Passwords does not match' : false
    }
  },
  
  watch: {
    password(val){
      if(this.registerForm){
        const ref = this.$refs.passwordAgain
        if(!!ref)
          ref.validate(true)
      }
    },
    passwordAgain(val){
      if(this.registerForm){
        const ref = this.$refs.password
        if(!!ref)
          ref.validate(true)
      }
    }
  },
  // Sends action to Vuex that will log you in and redirect to the dash otherwise, error
  methods: {
    login: function () {
      if(!this.$refs.form.validate()) return
      let username = this.username
      let password = this.password
      this.$store.dispatch('customerLogin', { username, password })
        .then(() => {
           const logged = this.$store.state.customerLogged
           if (logged) {

            this.snackbar.color="green",
            this.snackbar.msg = "Successfully logged in. Redirecting..."
            this.snackbar.show = true
            setTimeout(() => {
              this.$router.push('/')
            }, 2000);
           }
           else{
            this.snackbar.color="red",
            this.snackbar.msg = "Wrong username or password."
            this.snackbar.show = true
           }
          })
        .catch(err => {
          this.snackbar.color = "red"
          this.snackbar.msg = "Login Error."
          this.snackbar.show = true
        }
        )
    },
    register: function () {
      if(!this.$refs.form.validate()) return
      let username = this.username
      let password = this.password
      this.$store.dispatch('customerRegister', { username, password })
        .then(()=>{
          this.setRegisterForm(false)
          this.snackbar.color="green",
          this.snackbar.msg = "Successfully registered."
          this.snackbar.show = true
        })
        .catch(err => {
          this.snackbar.color="red",
          this.snackbar.msg = "Register Error."
          this.snackbar.show = true
        })
    },
    setRegisterForm (x){
      let elems = [
        this.$refs.username,
        this.$refs.password
      ]
      if(x == true){
        this.passwordRules.push(this.passwordMatchRule)
        this.passwordAgainRules.push(this.passwordMatchRule)
      }
      else{
        this.passwordRules.pop()
        this.passwordAgainRules.pop()
        elems.push(this.$refs.passwordAgain)
      }
      
      this.password=''
      this.passwordAgain=''
      this.username=''
      elems.forEach(element => {
        element.resetValidation()
      });
      this.registerForm = x
    }
  }
}
</script>

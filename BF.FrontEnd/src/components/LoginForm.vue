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
              color="general">
              <v-toolbar-title>User Panel</v-toolbar-title>
              <v-spacer/>
            </v-toolbar>
            <v-card-text>
              <v-form ref="form" >
                <v-text-field
                  ref="username"
                  v-model="username"
                  :rules="[() => !!username || 'This field is required']"
                  prepend-icon="mdi-account"
                  label="Username"
                  placeholder="yuzirneym"
                  required
                />
                <v-text-field
                  ref="password"
                  v-model="password"
                  :rules="[() => !!password || 'This field is required']"
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
              </v-form>
            </v-card-text>
            <v-divider class="mt-5"/>
            <v-card-actions>
              <v-spacer/>
              <v-btn
                align-center
                justify-center
                color="general"
                @click="login">Login
              </v-btn>
            </v-card-actions>
            <v-snackbar
              v-model="snackbar.show"
              :color="snackbar.color"
              :bottom="true"
            >
              {{ snackbar.msg }}
              <v-btn
                dark
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
      snackbar: {show:false,msg:"",color:""},
      showPassword: false
    }
  },

  methods: {
    login: function () {
      if(!this.$refs.form.validate()) return
      let username = this.username
      let password = this.password
      this.$store.dispatch('login', { username, password })
        .then(() => {
           const logged = this.$store.state.logged
           if (logged) {

            this.snackbar.color="green",
            this.snackbar.msg = "Successfully logged in. Redirecting..."
            this.snackbar.show = true
            setTimeout(() => {
              this.$router.push('/panel/dashboard')
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
    }
  },
  metaInfo () {
    return {
      title: 'Super Secret | Login'
    }
  }
}
</script>

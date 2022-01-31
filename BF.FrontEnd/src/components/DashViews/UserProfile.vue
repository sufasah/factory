<template>
  <v-container
    fill-height
    fluid
    grid-list-xl>
    <v-layout
      justify-center
      align-items-center
      wrap
    >
      <v-flex
        xs12
        md12
      >
        <material-card
          color="general"
          title="User Profile"
          text="Complete your profile"
        >
          <v-form ref="form">
            <v-container py-0>
              <v-layout wrap>
                
                <v-flex
                  xs12
                  md4
                >
                  <v-text-field
                    v-model="username"
                    ref="username"
                    class="purple-input"
                    label="Username"
                    :rules="usernameRules"
                    prepend-icon="mdi-account"
                    placeholder="yuzirneym"
                    required
                  />
                </v-flex>
                <v-flex
                  xs12
                  md4
                >
                  <v-text-field
                    v-model="password"
                    ref="password"
                    label="Password"
                    type="password"
                    class="purple-input"
                    :rules="passwordRules"
                    :append-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
                    :type="showPassword ? 'text' : 'password'"
                    prepend-icon="mdi-lock"
                    placeholder="*********"
                    counter
                    required
                    @keydown.enter="update"
                    @click:append="showPassword = !showPassword"
                    />
                </v-flex>
                <v-flex
                  xs12
                  md4
                >
                  <v-text-field
                    v-model="passwordAgain"
                    label="Password again"
                    ref="passwordAgain"
                    :rules="passwordAgainRules"
                    :append-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
                    :type="showPassword ? 'text' : 'password'"
                    prepend-icon="mdi-lock"
                    placeholder="*********"
                    counter
                    required
                    @keydown.enter="update"
                    @click:append="showPassword = !showPassword"/>
                </v-flex>

                <v-flex
                  xs12
                  text-xs-right
                >
                  <v-btn
                    class="mx-0 font-weight-light"
                    color="general"
                    @click="update"
                  >
                    Update Profile
                  </v-btn>
                </v-flex>
              </v-layout>
            </v-container>
          </v-form>
        </material-card>
      </v-flex>
    </v-layout>
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
  </v-container>
</template>

<script>
export default {
  data: function () {
    return {
      username:"",
      password:"",
      passwordAgain: '',
      showPassword:false,
      snackbar: {show:false,color:"",msg:""},
      usernameRules: [() => !!this.username || 'This field is required'],
      passwordRules : [() => !!this.password || 'This field is required',() => this.password!=this.passwordAgain ? 'Passwords does not match' : false],
      passwordAgainRules : [() => !!this.passwordAgain || 'This field is required',() => this.password!=this.passwordAgain ? 'Passwords does not match' : false],
    }
  },
  watch: {
    password(val){
      const ref = this.$refs.passwordAgain
      if(!!ref)
        ref.validate(true)
    },
    passwordAgain(val){
      const ref = this.$refs.password
      if(!!ref)
        ref.validate(true)
    }
  },
  methods:{
    update () {
      if(!this.$refs.form.validate()) return
      let username = this.username
      let password = this.password
      this.$store.dispatch('updateUser', { username, password })
        .then(() => {
          this.snackbar.color="green",
          this.snackbar.msg = "Successfully Updated"
          this.snackbar.show = true
        })
        .catch(err => {
          this.snackbar.color = "red"
          this.snackbar.msg = "Server error occured. Check response from network. User could not be updated."
          this.snackbar.show = true
        }
        )
    }
  }
}
</script>

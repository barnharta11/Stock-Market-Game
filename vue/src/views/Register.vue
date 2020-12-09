<template>
  <div id="register" class="container">
    <form class="form-register" @submit.prevent="register">
      <div id="header" class="maintext">Create Account</div>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <label id="box1-1" for="username" class="textclass">Username</label>
      <input type="text" id="box1-2" class="formclass" placeholder="Username..." v-model="user.username" required autofocus/>
      <label id="box2-1" for="email" class="textclass">Email</label>
      <input type="text" id="box2-2" class="formclass" placeholder="Email..." v-model="user.email" required autofocus/>
      <label id="box3-1" for="password" class="textclass">Password</label>
      <input type="password" id="box3-2" class="formclass" placeholder="Password..." v-model="user.password" required/>
      <input type="password" id="box3-3" class="formclass" placeholder="Confirm Password..." v-model="user.confirmPassword" required/>
      <label id="end" class="textclass"></label>
      <router-link id="box4-1" :to="{ name: 'login' }">Have an account?</router-link>
      <button id="button" class="btn" type="submit">Create Account</button>
    </form>
  </div>
</template>

<script>
import authService from '../services/AuthService';

export default {
  name: 'register',
  data() {
    return {
      user: {
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        role: 'user',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: '/login',
                query: { registration: 'success' },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style>


#register{
  background-color: rgb(105, 172, 105);
}

.form-register{
  display: grid;
  grid-template-columns: 1fr 2fr 2fr 1fr;
  /* grid-gap: 3px; */
  grid-template-areas: 
  ". header header ."
  ". usename usename ."
  ". usenamein usenamein ."
  ". email email ."
  ". emailin emailin ."
  ". pass pass ."
  ". passin passin ."
  ". passcon passcon ."
  ". formbottom formbottom ."
  ". createbutton loginlink .";
}

.maintext{
  font-family: Consolas, Arial, Helvetica;
  /* border-radius: 25px; */
  border-top-right-radius: 25px;
  border-top-left-radius: 25px;
  font-size: 50px;
  text-align: center;
  color: rgb(105, 172, 105);
  font-weight: bolder;
  line-height: 50px;
  background-color: lightgray;
  /* margin-bottom: 15px; */
  margin-top: 10px;
  padding-bottom: 25px;
  padding-top: 15px;
}

.textclass{
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 4px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
}

#end{
  font-family: Consolas, Arial, Helvetica;
  border-bottom-right-radius: 25px;
  border-bottom-left-radius: 25px;
  padding-top: 35px;
  padding-bottom: 3px;
  padding-left: 4px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  grid-area: formbottom;
}

.formclass{
  font-family: Consolas, Arial, Helvetica;
  border-color: gray;
  font-size: 28px;
  color: rgb(13, 42, 13);
  background-color: lightgray;
  line-height: 20px;
}

#box4-1{
  grid-area: loginlink;
  text-align: right;
  font-size: 25px;
  bottom: 0%;
  margin-top: 20px;

}

#button{
  grid-area: createbutton;
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  padding: 10px 20px;
  font-size: 20px;
  line-height: 20px;
  width: 50%;
  margin-top: 20px;
  margin-bottom: 100%;
  transition-duration: .4s;
}
#button :hover{
  background-color: white;
  color: black;
}

#header{
  grid-area: header;
}

#box1-1{
  grid-area: usename;
}
#box1-2{
  grid-area: usenamein;
}
#box2-1{
  grid-area: email;
}
#box2-2{
  grid-area: emailin;
}
#box3-1{
  grid-area: pass;
}
#box3-2{
  grid-area: passin;
}
#box3-3{
  grid-area: passcon;
}

</style>

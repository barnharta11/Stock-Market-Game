<template>
  <div>
      <div v-for="user in this.$store.state.allUsers" v-bind:key="user.id">{{user.userName}}</div>
  </div>
</template>

<script>
import userService from '../services/UserService.js'
export default {
methods:{
    loadUsers(){
        userService.listAllUsers()
        .then(response=>{
            this.$store.commit("SET_ALLUSERS", response.data)
        })
    }
}
}
</script>

<style>

</style>
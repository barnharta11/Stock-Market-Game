<template>
  <div>
      <div v-for="user in this.$store.state.allUsers" v-bind:key="user.id">
          {{user.username}} | 
          <button v-on:click='invitePlayer(2, 9)'>Invite Player</button>
          </div>
      
  </div>
</template>

<script>
import userService from '../services/UserService.js'
import inviteService from '../services/InviteService.js'
export default {
    data(){
        return{
            currentGame:[],
            invite: {
                userId: "",
                gameId: ""
            }
        }
    },
methods:{
    getCurrentGame(){
        this.currentGame=this.$store.state.selectedGame
    },
    loadUsers(){
        userService.listAllUsers()
        .then(response=>{
            this.$store.commit("SET_ALLUSERS", response.data)
        })
    },
    invitePlayer(userid, gameid){
        this.invite.userId=userid
        this.invite.gameId=gameid
        inviteService.inviteUser(this.invite)
    }
},
created(){
    this.loadUsers()
    this.getCurrentGame()
}
}
</script>

<style>

</style>
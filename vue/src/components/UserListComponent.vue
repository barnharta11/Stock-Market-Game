<template>
  <div>
      <div v-for="user in this.$store.state.allUsers" v-bind:key="user.UserId">
          {{user.username}} | 
          <button v-on:click="invitePlayer(user.UserId, this.currentGame.gameId)">Invite Player</button>
          </div>
      
  </div>
</template>

<script>
import userService from '../services/UserService.js'
import inviteService from '../services/InviteService.js'
export default {
    data(){
        return{
            currentUser:[],
            currentGame:[],
            invite: {
                userId: "",
                gameId: ""
            }
        }
    },
methods:{
    getCurrentUser(){
        this.currentUser=this.$store.state.user
    },
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
        .then(alert(`Invite was sent to ${this.user.username}`))
    }
},
created(){
    this.loadUsers()
    this.getCurrentGame()
    this.getCurrentUser()
}
}
</script>

<style>

</style>
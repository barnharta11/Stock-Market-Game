<template>
  <div id="invitecontainer" class="mainbackground">
      <span id="invitename" class="textclass" v-for="user in $store.state.allUsers" v-bind:key="user.userId">
          {{user.username}}</span>
          <button id="invitebtn" class="buttondefault" v-on:click="invitePlayer(user.userId, currentGame.gameId, user.username)">Invite Player</button>
          <!-- </span> -->
      
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
    invitePlayer(userid, gameid, username){
        this.invite.userId=userid
        this.invite.gameId=gameid
        inviteService.inviteUser(this.invite)
        .then(alert(`Invite was sent to ${username}`))
        .catch(err => {
            console.log(err)
            alert('There was an error')
        })
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

#invitecontainer{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr;
    grid-template-areas: 
    ". name btn .";
}

#invitename{
    grid-area: name;
    padding-right: 10px;
    border-right: 5px solid lightgray;
    color: lightgray;
    background-color: rgb(105,172,105);
    height:45px;
    text-align: right;
    
}

#invitebtn{
    grid-area: btn;
    height: 45px;
    font-size: 35px;
}

</style>
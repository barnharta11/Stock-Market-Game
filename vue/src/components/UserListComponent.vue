<template>
  <div id="invitecontainer" class="mainbackground">
      <input id="searchInput" type=text v-model='currentSearch' placeholder="Search User Name" />
        <div id="test">
            <div id="invitename" class="textclass" v-for="user in searchedUsers" v-bind:key="user.userId">
                <span>{{user.username}} |</span>
                <button id="invitebtn" class="buttondefault" v-on:click="invitePlayer(user.userId, currentGame.gameId, user.username)">Invite Player</button>
            </div>
        </div>
    </div>
</template>

<script>
import userService from '../services/UserService.js'
import inviteService from '../services/InviteService.js'
export default {
    data(){
        return{
            currentSearch:"",
            currentUser:[],
            currentGame:[],
            invite: {
                userId: "",
                gameId: ""
            }
        }
    },
computed:{
    searchedUsers(){
        let filtered = this.$store.state.allUsers
        if(this.currentSearch!=""){
            filtered=filtered.filter((user)=>
            user.username
            .toLowerCase()
            .includes(this.currentSearch.toLowerCase())
            )
        }
        return filtered
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

#test{
    text-align: right;
}

#invitecontainer{
    text-align: center;
}

#searchInput{
  font-family: Consolas, Arial, Helvetica;
  border: none;
  font-size: 28px;
  color: rgb(13, 42, 13);
  background-color: lightgray;
  line-height: 20px;
  padding-left: 15px;
  /* align-content: center; */
}
#invitename{
    grid-area: name;
    padding-right: 10px;
    color: lightgray;
    background-color: rgb(105,172,105);
    height:45px;
    text-align: right;
    padding-right: 30%;
}

#invitebtn{
    grid-area: btn;
    height: 45px;
    font-size: 35px;
    width: 30%
}

</style>
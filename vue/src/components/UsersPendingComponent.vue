<template>
<div class="mainbackground">
    <div id="pendingcontainer">
        <h2 id="tablehead3" class="tableheader">My Pending Games</h2>
        <table id="pendingtable" class="tablebody">
            <thead>
                <tr>
                    <th class="columnheader">Name</th>
                    <th class="columnheader">Creator</th>
                    <th class="columnheader">Start Date</th>
                    <th class="columnheader">End Date</th>
                    <th class="columnheaderend">Status</th>
                </tr>
            </thead>
            <tbody v-for="game in pendingGames" v-bind:key="game.id">
                <tr>
                    <td class="itemstyle" v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td class="itemstyle">{{game.creatorName}}</td>
                    <td class="itemstyle">{{game.formattedStartDate}}</td>
                    <td class="itemstyle">{{game.formattedEndDate}}</td>
                    <td class="itemstyle">{{game.statusName}}</td>
                    <td class="itemstyleend"><button class="buttondefault" v-on:click="AcceptInvite(game.gameId)" >Accept</button></td>
                </tr>
            </tbody>
        </table>
        <label id="end3" class="tablefoot"></label>
    </div>
</div>
</template>

<script>
import inviteService from '../services/InviteService.js'
import gameService from "../services/GameService.js"
export default {
data(){
        return{
            pendingGames: [],
            invite: {
                userId: "",
                gameId: ""
            },
            arrayOfGameID:[]
        }
    },
    computed:{
        // FilterForPending(){
            
            
            


            

            
                    
                
           







            // I want to find all the games where the leaderboard list has an entry that has a status of pending
           
        // }
    },
    methods:{
        SetFilteredList(){
            let arrayToFilter = this.$store.state.userGames
            arrayToFilter.forEach(game=>{
                game.leaderboardList.forEach(element=>{
                    if (element.playerStatus=="Pending" && element.userName==this.$store.state.user.username){
                        this.arrayOfGameID.push(parseInt(element.gameID))
                    }
                })
            })
            arrayToFilter.forEach(game=>{
                if(this.arrayOfGameID.includes(game.gameId)){
                    this.pendingGames.push(game)
                }
            })
        },
    AcceptInvite(gameid){
        this.invite.userId=this.$store.state.user.userId
        this.invite.gameId=gameid
        inviteService.acceptInvite(this.invite)
        .then(
            this.pendingGames=[],
            this.GetUsersGames(),
              this.$router.push('/')
        )
    },
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/games/leaderboard/${this.$store.state.selectedGame.gameId}`)

    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            this.$store.commit("SET_USERSGAMES", response.data)
            response.data.forEach(element => {
                if(element.statusName=='Pending'){
                    this.pendingGames.push(element)
                  
                }
            });
        })
    }
},
created(){
    this.GetUsersGames()
    this.SetFilteredList()

}
}
</script>


<style>

#pendingcontainer{
    
    display: grid;
    grid-template-columns: 1fr 8fr 1fr;
    grid-template-areas:
    ". thead ."
    ". tbody ."
    ". footer .";
}

#tablehead3{
    grid-area: thead;
}

#pendingtable{
    grid-area: tbody;
}

#end3{
    grid-area: footer;
}

.tablefoot{
  padding-top: 25px;
  border-bottom-right-radius: 25px;
  border-bottom-left-radius: 25px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
}

</style>
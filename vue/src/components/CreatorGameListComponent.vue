<template>

<div class="mainbackground">
    <div id="mycreatedgamescontainer">
        <h2 id="tablehead4" class="tableheader">My Created Games</h2>
        <table id="createdgamestable" class="tablebody">
            <thead>
                <tr>
                    <th class="columnheader">Name</th>
                    <th class="columnheader">Creator</th>
                    <th class="columnheader">Start Date</th>
                    <th class="columnheader">End Date</th>
                    <th class="columnheader">Status</th>
                    <th class="columnheaderend">Add Others</th>
                </tr>
            </thead>
            <tbody v-for="game in creatorGames" v-bind:key="game.id">
                <tr>
                    <td class="itemstyle" id="routegamename" v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td class="itemstyle">{{game.creatorName}}</td>
                    <td class="itemstyle">{{game.formattedStartDate}}</td>
                    <td class="itemstyle">{{game.formattedEndDate}}</td>
                    <td class="itemstyle">{{game.statusName}}</td>
                    <button class="buttondefault" v-on:click="SetInviteGame(game)">Invite Users</button>
                    <!-- <td><router-link class="itemstyleend" v-on:click="SetInviteGame(game)" :to="{name:'displayusers'}">Invite users</router-link></td> -->
                </tr>
            </tbody>
        </table>
        <label id="end4" class="tablefoot"></label>
    </div>  
</div>

</template>

<script>
import gameService from "../services/GameService.js"
export default {
    data(){
        return{
            creatorGames: [],

        }
    },
methods:{
    SetInviteGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push('/inviteusers')
    },
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game);
        this.$router.push(`/games/leaderboard/${this.$store.state.selectedGame.gameId}` );
    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            response.data.forEach(element => {
                if(element.creatorName==this.$store.state.user.username){
                    this.creatorGames.push(element)
                }
            });
        })
    }
},
created(){
    this.GetUsersGames()

}
}
</script>

<style>

#mycreatedgamescontainer{
    display: grid;
    grid-template-columns: 1fr 8fr 1fr;
    grid-template-areas: 
    ". thead ."
    ". tbody ."
    ". footer ."
    ;
}

#tablehead4{
    grid-area: thead;
}

#createdgamestable{
    grid-area: tbody;
}

#end4{
    grid-area: footer;
}

.mediumtextclass{
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
}

#gamelistcreatorname{
    grid-area: creatorname;
    border-right: 2px solid rgb(105, 172, 105);
}

#gameliststartdate{
    grid-area: startdate;
    border-right: 2px solid rgb(105, 172, 105);

}

#gamelistenddate{
    grid-area: enddate;
    border-right: 2px solid rgb(105, 172, 105);

}

#gameliststatus{
    grid-area: status; 
    border-right: 2px solid rgb(105, 172, 105);

}

#gamelistid{
    grid-area: id;
}

#button2{
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  /* padding: 10px 20px; */
  font-size: 20px;
  /* line-height: 20px; */
  /* width: 50%; */
  margin: 5px;
  /* margin-bottom: 100%; */
  transition-duration: .4s;
  grid-area: button;
}

#routegamename{
    text-decoration: underline;
}

.route:link, .route:visited{
    color: rgb(105, 172, 105);
    text-decoration: underline;
}

.route:hover{
    color: black;
    text-decoration: underline;
}

</style>


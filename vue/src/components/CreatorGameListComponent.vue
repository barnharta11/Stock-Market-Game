<template>

<div class="mainbackground">
    <div id="contain">
        <h2>My Created Games</h2>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Creator</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Status</th>
                    <th>Add Others</th>
                </tr>
            </thead>
            <tbody v-for="game in creatorGames" v-bind:key="game.id">
                <tr>
                    <td id="routegamename" class="route" v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td>{{game.creatorName}}</td>
                    <td>{{game.startDate}}</td>
                    <td>{{game.endDate}}</td>
                    <td>{{game.statusName}}</td>
                    <router-link class="route" v-on:click="SetInviteGame(game)" :to="{name:'displayusers'}"><td>Invite users</td></router-link>
                </tr>
            </tbody>
        </table>
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
    },
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game);
        this.$router.push(`/assets/${this.$store.state.user.userId}/${game.gameId}` );
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

#contain{
    display: grid;
    grid-template-columns: 1fr 3fr 1fr 3fr 3fr 1fr .5fr 1fr;
    grid-template-areas: 
    /* ". . top top top top top top ." */
    ". button creatorname startdate enddate status id .";
}

.mainbackground{
    font-family: Consolas, Arial, Helvetica;
    padding-top: 25px;
    background-color: rgb(105,172,105);
    height: 100vh;
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


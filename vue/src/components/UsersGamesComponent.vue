<template>

<div class="mainbackground">
    <div id="gamelistcontainer">
        <h2>My Current Games</h2>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Creator</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody id="usersgamescontainer" v-for="game in this.$store.state.userGames" v-bind:key="game.id">
                <tr>
                    <td v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td>{{game.creatorName}}</td>
                    <td>{{game.startDate}}</td>
                    <td>{{game.endDate}}</td>
                    <td>{{game.statusName}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

</template>

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push({name:'assetdisplay', params: {gameid:game.gameId, userid:this.$store.state.user.userid} })
    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            this.$store.commit("SET_USERSGAMES", response.data)
        })
    }
},
created(){
    this.GetUsersGames()

}
}
</script>

<style>


#usersgamescontainer{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 2.5fr 2.5fr 1fr 1fr;
    grid-template-areas: 
    ". buttonslot name start end status .";
    /* padding: 100px; */
}

.buttondefault{
  grid-area: buttonslot;
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  /* padding: 10px 20px; */
  font-size: 20px;
  /* line-height: 20px; */
  width: 80%;
  margin: 10px;
  /* margin-bottom: 10px ; */
  transition-duration: .4s;
}

#currentname{
    grid-area: name;
        border-right: 2px solid rgb(105, 172, 105);

}

#currentstart{
    grid-area: start;
    border-right: 2px solid rgb(105, 172, 105);
}

#currentend{
    grid-area: end;
    border-right: 2px solid rgb(105, 172, 105);
}

#currentstatus{
    grid-area: status;
}

</style>
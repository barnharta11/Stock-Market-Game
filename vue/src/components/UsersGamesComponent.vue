<template>

<div class="testfix">
    <div id="gamelistcontainer">
        <h2 id="tablehead1" class="tableheader">My Current Games</h2>
        <table class="tablebody">
            <thead>
                <tr>
                    <th class="columnheader">Name</th>
                    <th class="columnheader">Creator</th>
                    <th class="columnheader">Start Date</th>
                    <th class="columnheader">End Date</th>
                    <th class="columnheaderend">Status</th>
                </tr>
            </thead>
            <tbody v-for="game in this.$store.state.userGames" v-bind:key="game.id">
                <tr>
                    <td class="itemstyle" v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td class="itemstyle">{{game.creatorName}}</td>
                    <td class="itemstyle">{{game.formattedStartDate}}</td>
                    <td class="itemstyle">{{game.formattedEndDate}}</td>
                    <td class="itemstyleend">{{game.statusName}}</td>
                </tr>
            </tbody>
        </table>
        <label id="end1" class="tablefoot"></label>
    </div>
</div>

</template>

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/games/leaderboard/${this.$store.state.selectedGame.gameId}`)
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

.columnheaderend{
    text-align: center;
    font-size: 30px;
}

.columnheader{
    padding-bottom: 10px;
    text-align: center;
    border-right: 2px solid rgb(105, 172, 105);
    font-size: 30px;
}

.itemstyle{
    margin: 0%;
    padding-top: 0;
    font-size: 20px;
    text-align: center;
    border-right: 2px solid rgb(105, 172, 105);
}

.itemstyleend{
    font-size: 20px;
    text-align: center;
}

#tablehead1{
    font-size: 45px;
    grid-area: thead;
}

.tablebody{
    margin: 0;
    background-color: lightgray;
    color: rgb(105, 172, 105);
    border-right: rgb(105, 172, 105);
    grid-area: tbody;
}

#gamelistcontainer{
    display: grid;
    grid-template-columns: 1fr 8fr 1fr;
    grid-template-areas: 
    ". thead ."
    ". tbody ."
    ". foot .";
}

.buttondefault{
  grid-area: buttonslot;
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  font-size: 20px;
  width: 80%;
  margin: 10px;
  transition-duration: .4s;
}

.formfoot{
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
}

#end1{
    grid-area: foot;
}

</style>
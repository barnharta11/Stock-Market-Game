<template>
<div class="mainbackground">
    <div id="pendingcontainer">
        <h2 id="tablehead3" class="tableheader1">My Pending Games</h2>
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
                    <td class="itemstyle">{{game.startDate}}</td>
                    <td class="itemstyle">{{game.endDate}}</td>
                    <td class="itemstyle">{{game.statusName}}</td>
                    <td class="itemstyleend"><button class="buttondefault" >Accept</button></td>
                </tr>
            </tbody>
        </table>
        <label id="end3" class="tablefoot"></label>
    </div>
</div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
data(){
        return{
            pendingGames: [],

        }
    },
    methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/games/leaderboard/${this.$store.state.selectedGame.gameId}`)

    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
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
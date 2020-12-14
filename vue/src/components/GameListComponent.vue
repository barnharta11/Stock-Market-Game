<template>
<div class="mainbackground">
    <div id="allgamescontainer">
        <h2 id="tablehead2" class="tableheader1">All Games</h2>
        <table id="allgamelisttable" class="tablebody">
            <thead>
                <tr>
                    <th class="columnheader">Name</th>
                    <th class="columnheader">Creator</th>
                    <th class="columnheader">Start Date</th>
                    <th class="columnheaderend">End Date</th>
                </tr>
            </thead>
            <tbody v-for="game in this.$store.state.allGames" v-bind:key="game.id">
                <tr>
                    <td class="itemstyle" v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td class="itemstyle">{{game.creatorName}}</td>
                    <td class="itemstyle">{{game.startDate}}</td>
                    <td class="itemstyleend">{{game.endDate}}</td>
                </tr>
            </tbody>
        </table>
        <label id="end2" class="tablefoot"></label>
    </div>
</div>


</template> -->

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/games/leaderboard/${this.$store.state.selectedGame.gameId}`)
    },
    GetAllGames(){
        gameService.listAllGames()
        .then(response=>{
            this.$store.commit("SET_ALLGAMES", response.data)
        })
    }
},
created(){
    this.GetAllGames()
}
}
</script>

<style>

#allgamescontainer{
    display: grid;
    grid-template-columns: 1fr 8fr 1fr;
    grid-template-areas: 
    ". thead ."
    ". tbody ."
    ". foot .";
}

#tablehead2{
    grid-area: thead;
}

#allgamelisttable{
    grid-area: tbody;
}

#end2{
    grid-area: foot;
}

</style>
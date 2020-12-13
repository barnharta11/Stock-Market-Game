<template>
<div class="mainbackground">
    <div id="allgamestablecontainer" >
        <h2 id="tablehead2" class="tableheader1">All Games</h2>
        <table id="gamelistcreatorname" class="tablebody">
            <thead>
                <tr>
                    <th class="columnheader">Name</th>
                    <th class="columnheader">Creator</th>
                    <th class="columnheader">Start Date</th>
                    <th class="columnheader">End Date</th>
                    <th class="columnheaderend">Status</th>
                </tr>
            </thead>
            <tbody v-for="game in this.$store.state.allGames" v-bind:key="game.id">
                <tr>
                    <td class="itemstyle" v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td class="itemstyle">{{game.creatorName}}</td>
                    <td class="itemstyle">{{game.startDate}}</td>
                    <td class="itemstyle">{{game.endDate}}</td>
                    <td class="itemstyleend">{{game.statusName}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>


</template> -->

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/assets/${this.$store.state.user.userId}/${game.gameId}`)
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

#allgamestablecontainer{
    display: grid;
    grid-template-columns: 1fr 8fr 1fr;
    grid-template-areas: 
    ". thead2  ."
    ". tbody2 .";

}

#tablehead2{
    grid-area: thead2;
}

.tablebody{
    grid-area: tbody2;
}

</style>
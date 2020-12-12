<template>
<div class="mainbackground">
    <div id="gamelistcontainer" >
        <h2>All Games</h2>
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
            <tbody v-for="game in this.$store.state.allGames" v-bind:key="game.id">
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

#gamelistcontainer{
    background-color: rgb(105,172,105);
    display: grid;
    grid-template-columns: 1fr 1fr 2fr 2.5fr 2.5fr 1fr;
    grid-template-areas: 
    ". buttonslot name start end ."
    ;
}

#allname{
    grid-area: name;
    border-right: 2px solid rgb(105, 172, 105);
}

#allstart{
    grid-area: start;
            border-right: 2px solid rgb(105, 172, 105);

}

#allend{
    grid-area: end;
}

</style>
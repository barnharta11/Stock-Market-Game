<template>
<div class="mainbackground">
    <div id="gamelistcontainer" >
        <h2>My Pending Games</h2>
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
            <tbody id="userspendingcontainer" v-for="game in pendingGames" v-bind:key="game.id">
                <tr>
                    <td v-on:click='SetSelectedGame(game)'>{{game.gameName}}</td>
                    <td>{{game.creatorName}}</td>
                    <td>{{game.startDate}}</td>
                    <td>{{game.endDate}}</td>
                    <td>{{game.statusName}}</td>
                    <td><button class="buttondefault" >Accept</button></td>
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
            pendingGames: [],

        }
    },
    methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/assets/${this.$store.state.user.userId}/${game.gameId}`)

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

#userspendingcontainer{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 2fr 2fr 1fr 1fr 1fr;
    grid-template-areas:
    ". buttonslot name start end status id ."
    ;
}

#pendingname{
    grid-area: name;
    border-right: 2px solid rgb(105, 172, 105);

}

#pendingstart{
    grid-area: start;
    border-right: 2px solid rgb(105, 172, 105);

}

#pendingend{
    grid-area: end;
    border-right: 2px solid rgb(105, 172, 105);

}

#pendingstatus{
    grid-area: status;
    border-right: 2px solid rgb(105, 172, 105);

}

#pendingid{
    grid-area: id;
}



</style>
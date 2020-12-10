<template>
  <div>
      <div id="userspendingcontainer" v-for="game in pendingGames" v-bind:key="game.id">
          <!-- <router-link :to="{name:'gamedetails', params: {gameid:game.gameId}}"> -->
          <button class="buttondefault" v-on:click='SetSelectedGame(game)'>
              {{game.gameName}}
              </button>
              <!-- </router-link> -->
          <span id="pendingname" class="textclass">{{game.creatorName}}</span>
          <span id="pendingstart" class="textclass">{{game.startDate}}</span>
          <span id="pendingend" class="textclass">{{game.endDate}}</span>
          <span id="pendingstatus" class="textclass">{{game.statusName}}</span>
          <span id="pendingid" class="textclass">{{game.gameId}}</span>
          
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
        this.$router.push({name:'gamedetails', params: {gameid:game.gameId} })

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
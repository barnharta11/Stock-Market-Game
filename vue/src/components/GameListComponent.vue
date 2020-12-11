<template>
  <div>
  <!-- <div class="mainbackground"> -->
      <div id="gamelistcontainer" v-for="game in this.$store.state.allGames" v-bind:key="game.id">
<!-- <router-link :to="{name:'gamedetails', params: {gameid:game.gameId}}"> -->
          <!-- <button class="buttondefault" v-on:click='SetSelectedGame(game)'> -->
          <span>{{game.gameName}}</span>
            <!-- </button> -->
              <!-- </router-link> -->
          <span id="allname" class="textclass">{{game.creatorName}}</span>
          <span id="allstart" class="textclass">{{game.startDate}}</span>
          <span id="allend" class="textclass">{{game.endDate}}</span>
      </div>
      
  </div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push({name:'gamedetails', params: {gameid:game.gameId}})
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
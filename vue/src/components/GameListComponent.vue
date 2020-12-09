<template>
  <div>
      <div v-for="game in this.$store.state.allGames" v-bind:key="game.id">
<router-link :to="{name:'gamedetails', params: {gameid:game.gameId}}">
          <button v-on:click='SetSelectedGame(game)'>
          <span>{{game.gameName}} | </span>
 </button>
              </router-link>
          <span>{{game.creatorName}} | </span>
          <span>{{game.startDate}} | </span>
          <span>{{game.endDate}}</span>
      </div>
      
  </div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
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

</style>
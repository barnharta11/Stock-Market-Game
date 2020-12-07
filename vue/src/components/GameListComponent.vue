<template>
  <div>
      <div v-for="game in this.$store.state.allGames" v-bind:key="game.id">
          <span>{{game.gameName}} | </span>
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
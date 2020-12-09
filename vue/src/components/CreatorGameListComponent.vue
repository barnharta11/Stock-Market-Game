<template>
  <div>
      <div v-for="game in creatorGames" v-bind:key="game.id">
          <router-link :to="{name:'gamedetails', params: {gameid:game.gameId}}">
          <button v-on:click='SetSelectedGame(game)'>
              {{game.gameName}} | 
              </button>
              </router-link>
          <span>{{game.creatorName}} | </span>
          <span>{{game.startDate}} | </span>
          <span>{{game.endDate}} | </span>
          <span>{{game.statusName}} | </span>
          <span>{{game.gameId}}</span>
          
      </div>
  </div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
    data(){
        return{
            creatorGames: [],

        }
    },
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            response.data.forEach(element => {
                if(element.creatorName==this.$store.state.user.username){
                    this.creatorGames.push(element)
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

</style>
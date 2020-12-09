<template>
  <div>
      <div v-for="game in this.$store.state.userGames" v-bind:key="game.id">
          <span>{{game.gameName}} | </span>
          <span>{{game.creatorName}} | </span>
          <span>{{game.startDate}} | </span>
          <span>{{game.endDate}} | </span>
          <span>{{game.statusName}}</span>
          <span>
              <!-- <router-link :to -->
          </span>
      </div>
  </div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            this.$store.commit("SET_USERSGAMES", response.data)
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
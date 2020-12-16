<template>
  <div class="game">
    <div class="game-title">{{ Game.gameName }}</div>
    <div v-for="board in Game.leaderboardList" v-bind:key="board.userGameID" >
      <div>{{board.userName}}</div>
      <div>{{board.netWorth}}</div>
      
    </div>
<button v-on:click='SetSelectedGame(Game)'>Game Details</button>


    <!-- <div v-for="object in Game.leaderboardList" v-bind:key="object.userID">
      <div>{{ object.userName }}</div>
    </div> -->
    <!-- <router-link :to="`/assets/${this.$store.state.user.userId}/${object.gameID}`">game details</router-link> -->
  </div>
</template>

<script>
export default {
  props: {
    Game: Object,
  },
  data(){
    return{
      currentLeaderboardList:[]
    }
  },
  methods:{
    SetCurrentBoard(game){
      this.currentLeaderboardList=game
    },
          SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push(`/games/leaderboard/${this.$store.state.selectedGame.gameId}`)
    },
  },

  created(){
    this.SetCurrentBoard()
  }
};
</script>

<style>
</style>
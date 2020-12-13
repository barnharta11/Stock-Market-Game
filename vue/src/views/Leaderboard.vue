<template>
  <div>Loading Content</div>
</template>

<script>
import leaderboardService from '../services/LeaderboardService.js'
export default {
    
    methods:{
        GetGameLeaderboard(){
            leaderboardService.getGameLeaderboard(this.$store.state.selectedGame.gameId)
            .then(response=>{
                this.$store.commit("SET_CURRENT_LEADER_BOARD", response.data);
                this.$router.push(`/assets/${this.$store.state.user.userId}/${this.$store.state.selectedGame.gameId}`)
            }
            )
        }
    },
    created(){
        this.GetGameLeaderboard()
    }
}
</script>

<style>

</style>

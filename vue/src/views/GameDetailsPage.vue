<template>
  <div>
    <game-details-component/>
    <leaderboard-component />    
    <asset-component v-show="IsInGame()"/>
    <stock-universe-component v-show="IsInGame()"/>
  </div>
</template>

<script>
import GameDetailsComponent from "../components/GameDetailsComponent"
import AssetComponent from '../components/AssetComponent.vue'
import StockUniverseComponent from '../components/StockUniverseComponent.vue'
import LeaderboardComponent from '../components/LeaderboardComponent.vue'
export default {
    props:{
        game:[]
    },
    methods:{
      IsInGame(){
        return (this.$store.state.selectedGame.statusName=="Active")
      }
    },
components:{GameDetailsComponent,AssetComponent, StockUniverseComponent, LeaderboardComponent}
}
</script>

<style>

</style>,

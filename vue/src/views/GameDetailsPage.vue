<template>
  <div>
    <game-details-component/>
    <leaderboard-component/>    
    <asset-component/>
    <stock-universe-component/>
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
components:{GameDetailsComponent,AssetComponent, StockUniverseComponent, LeaderboardComponent}
}
</script>

<style>

</style>,

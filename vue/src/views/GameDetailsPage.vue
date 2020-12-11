<template>
  <div>
    <game-details-component/>
    <portfolio-component/>
  </div>
</template>

<script>
import GameDetailsComponent from "../components/GameDetailsComponent"
import PortfolioComponent from '../components/PortfolioComponent.vue'
export default {
    props:{
        game:[]
    },
components:{GameDetailsComponent,PortfolioComponent}
}
</script>

<style>

</style>
<template>
  <div>
    <game-details-component/>
    <asset-component/>
  </div>
</template>

<script>
import GameDetailsComponent from "../components/GameDetailsComponent"
import AssetComponent from '../components/AssetComponent.vue'
export default {
    props:{
        game:[]
    },
components:{GameDetailsComponent,AssetComponent}
}
</script>

<style>

</style>
<template>
  <div>
          <span>{{selectedGame.gameName}} | </span>
          <span>{{selectedGame.creatorName}} | </span>
          <span>{{selectedGame.startDate}} | </span>
          <span>{{selectedGame.endDate}}</span>
  </div>
</template>

<script>
export default {
  data(){
    return{
      selectedGame:[]
    }    
  },
  created(){
    this.selectedGame=this.$store.state.selectedGame
  }
  }


</script>

<style>

</style>
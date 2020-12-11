<template>
  <div>
          <span>{{selectedGame.gameName}} | </span>
          <span>{{selectedGame.creatorName}} | </span>
          <span>{{selectedGame.startDate}} | </span>
          <span>{{selectedGame.endDate}}</span>
          
          <!-- Display User's Portfolio (Pass current userID(from$store) to controller)-->
              <!-- Display Balance and Net Balance on one line -->
              <!-- Stocks in a table -->




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
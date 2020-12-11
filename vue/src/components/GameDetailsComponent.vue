<template>
  <div>
          <span>{{selectedGame.gameName}} | </span>
          <span>{{selectedGame.creatorName}} | </span>
          <span>{{selectedGame.startDate}} | </span>
          <span>{{selectedGame.endDate}}</span>
<<<<<<< HEAD
          <!-- Display User's Portfolio (Pass current userID(from$store) to controller)-->
              <!-- Display Balance and Net Balance on one line -->
              <!-- Stocks in a table -->

=======
<<<<<<< HEAD
          <!-- remove this -->
          <router-link :to="{name:'displayusers'}">Invite users</router-link>
=======
          <!-- <router-link :to="{name:'displayusers'}">Invite users</router-link> -->
>>>>>>> ba4e35e74da023b8475edde0c4626cf125c7eafd
>>>>>>> 1bb3e9584293d609ca763ed63a45044a757834ce
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
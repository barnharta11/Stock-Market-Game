<template>
  <div>
          <span>{{selectedGame.gameName}} | </span>
          <span>{{selectedGame.creatorName}} | </span>
          <span>{{selectedGame.startDate}} | </span>
          <span>{{selectedGame.endDate}}</span>
<<<<<<< HEAD
          <!-- remove this -->
          <router-link :to="{name:'displayusers'}">Invite users</router-link>
=======
          <!-- <router-link :to="{name:'displayusers'}">Invite users</router-link> -->
>>>>>>> ba4e35e74da023b8475edde0c4626cf125c7eafd
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
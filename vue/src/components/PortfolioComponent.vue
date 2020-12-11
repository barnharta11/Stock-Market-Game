<template>
  <div>
      <span>{{this.$store.state.activePortfolio.balance}}</span>
      <span>{{netWorth}}</span>
  </div>
</template>

<script>
import portfolioService from '../services/PortfolioService.js'
export default {
    methods:{
        GetUserPortfolio(userId, gameId){
            portfolioService.GetUserPortfolio(userId, gameId)
            .then(response=>{
                this.$store.state.activePortfolio=response.data
            })
        }
    },
    calculated:{
        netWorth(){
            return "We will add this later"
        }
    },
    created(){
        this.GetUserPortfolio
    }

}
</script>

<style>

</style>
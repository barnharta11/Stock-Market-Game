<template>
  
      <div class="mainbackground">
    <!-- <div id="" >
        <h2> {username}'s Portfolio</h2>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Net Worth</th>
                    <th>Outstanding Cash</th>
                    <th>Game Ends</th>
                </tr>
            </thead>
            <tbody v-for="" v-bind:key="">
                <tr>
                    <td {{this.$store.state.activePortfolio.balance}}>{{netWorth}}</td>
                    <td>{{- outstanding cash -}}</td>
                    <td>{{- game ends -}}</td>
                </tr>
            </tbody>
        </table>
        <h3> {username}'s Stocks</h3>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Stock Ticker</th>
                    <th>Company Name</th>
                    <th>Current Price</th>
                    <th>Purchased Price</th>
                    <th>Quantity Held</th>
                    <th>Buy/Sell</th>
                </tr>
            </thead>
            <tbody v-for="" v-bind:key="">
                <tr>
                    <td>{{- stock ticker -}}</td>
                    <td>{{- company name -}}</td>
                    <td>{{- current price -}}</td>
                    <td>{{- purchased price -}}</td>
                    <td>{{- buy/sell pop up -}}</td>
                </tr>
            </tbody>
        </table>
    </div> -->
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
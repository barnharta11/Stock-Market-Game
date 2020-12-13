<template>
  <div class="mainbackground">
    <div id="tbd" >
        <h2> {{$store.state.user.username}}'s Portfolio</h2>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Net Worth</th>
                    <th>Game Ends</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>{{NetWorth()}} | </td>
                    <td>{{$store.state.selectedGame.endDate}}</td>
                </tr>
            </tbody>
        </table>
        <h3> {{$store.state.user.username}}'s Stocks</h3>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Stock Ticker</th>
                    <th>Company Name</th>
                    <th>Current Price</th>
                    <!-- <th>Purchased Price</th> -->
                    <th>Quantity Held</th>
                    <th>Total Value</th>
                    <th>Buy/Sell</th>
                </tr>
            </thead>
            <tbody v-for="asset in $store.state.activeAssets" v-bind:key="asset.portfolioId">
                <tr>
                    <td>{{asset.symbol}}</td>
                    <td>{{asset.companyName}}</td>
                    <td>{{asset.currentPrice}}</td>
                    <td>{{asset.quantityHeld}}</td>
                    <td>${{TotalEquity(asset).toFixed(2)}}</td>
                    <!-- <td>{{- purchased price -}}</td> -->
                    <!-- <td>{{- buy/sell pop up -}}</td> -->
                </tr>
            </tbody>
        </table>
    </div>
  </div>
  

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
<!-- </div>  -->

</template>

<script>
import assetService from '../services/AssetService.js'
export default {
    data(){
        return{
            
        }
    },
    methods:{
        SetUserAssets(){
            assetService.getUserAssets(this.$store.state.user.userId, this.$store.state.selectedGame.gameId)
            .then(response=>{
                this.$store.commit('SET_ALL_STOCKS', response.data[0])
                this.$store.commit("SET_SELECTED_ASSETS", response.data[1]);
            })
        },
        TotalEquity(asset){
            let assetEquity=(asset.quantityHeld * asset.currentPrice)
            this.netWorth+=assetEquity
            return assetEquity
        },
        NetWorth(){
            let returnWorth=0
            this.$store.state.activeAssets.forEach(element => {
                returnWorth += (element.quantityHeld*element.currentPrice)
            });
            return returnWorth.toFixed(2)
        }
    },
    computed:{

    },
    created(){
        this.SetUserAssets()
        // this.$store.state.user.userId, this.$store.state.selectedGame.gameId
    }

}
</script>

<style>

</style>
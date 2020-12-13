<template>
  <div class="mainbackground">
    <div id="contain">
        <h2>Stock Universe Menu</h2>
        <input id="searchInput" type=text v-model='currentSearch' placeholder="Search Stock Name" />
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Symbol</th>
                    <th>Company Name</th>
                    <th>Current Price</th>
                    <th>Buy</th>
                    <th>Last Purchase</th>
                </tr>
            </thead>
            <tbody v-for="stock in searchedStocks" v-bind:key="stock.assetId">
                <!-- <tbody> -->
                <tr>
                    <td>{{stock.symbol}}</td>
                    <td>{{stock.companyName}}</td>
                    <td>{{stock.currentPrice}}</td>
                    <td>
                        <button v-on:click="PromptForPurchase(stock)">
                            Purchase
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>  
</div>
</template>

<script>
import assetService from '../services/AssetService'
export default {
    data(){
        return{
            currentSearch:"",
            popupActive:false,
            quantityToBuy: "",
            transactionRequest:{
                portfolioId:'',
                quantityAdjustment:'',
                USDAdjustment:'',
                assetId:''
            },
            existed:false
        }
        
    },
    methods:{
        ToggleActive(){
            this.popupActive=!this.popupActive
        },
        PromptForPurchase(stock){
            this.quantityToBuy = prompt('How many would you like to purchase?', "");
            if((stock.currentPrice*this.quantityToBuy)<=(this.$store.state.activeAssets[0].quantityHeld)){
                this.transactionRequest.portfolioId=this.$store.state.activeAssets[0].portfolioID
                this.transactionRequest.quantityAdjustment=this.quantityToBuy
                this.transactionRequest.USDAdjustment=(stock.currentPrice*this.quantityToBuy*-1)
                this.transactionRequest.assetId=stock.assetId
                
                this.$store.state.activeAssets.forEach(asset =>{
                    if (asset.assetId==stock.assetId){
                        assetService.buyExistingStocks(this.$store.state.user.userId, this.$store.state.selectedGame.gameId, this.transactionRequest)
                        this.existed=true;
                    }               
                })
                if(this.existed==false){
                    assetService.buyNewStocks(this.$store.state.user.userId, this.$store.state.selectedGame.gameId, this.transactionRequest)
                }
                assetService.getUserAssets(this.$store.state.user.userId, this.$store.state.selectedGame.gameId)
            }
        }
    },
    computed:{
        searchedStocks(){
            let filtered = this.$store.state.allStocks
            if(this.currentSearch!=""){
                filtered=filtered.filter((stock)=>
                stock.companyName
                .toLowerCase()
                .includes(this.currentSearch.toLowerCase()))
            }
            return filtered
        }
    }

   

}
</script>

<style>

</style>
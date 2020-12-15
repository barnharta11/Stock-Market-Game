<template>
  <div class="testfix">
    <div id="portfoliocontainer">
      <h2 id="leaderboardhead" class="tableheader1">{{ $store.state.user.username }}'s Portfolio</h2>
      <table id="portfoliobody" class="tablebody">
        <thead>
          <tr>
            <th class="columnheader">Net Worth</th>
            <th class="columnheaderend">Game Ends</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td class="itemstyle">{{ NetWorth() }}</td>
            <td class="itemstyleend">{{ $store.state.selectedGame.formattedEndDate }}</td>
          </tr>
        </tbody>
      </table>
      <h3 id="stockshead" class="tableheader1">{{ $store.state.user.username }}'s Stocks</h3>
      <table id="stocksbody" class="tablebody">
        <thead>
          <tr>
            <th class="columnheader">Stock Ticker</th>
            <th class="columnheader">Company Name</th>
            <th class="columnheader">Current Price</th>
            <!-- <th>Purchased Price</th> -->
            <th class="columnheader">Quantity Held</th>
            <th class="columnheader">Total Value</th>
            <th class="columnheaderend">Buy/Sell</th>
          </tr>
        </thead>
        <tbody
          v-for="asset in $store.state.activeAssets"
          v-bind:key="asset.portfolioId"
        >
          <tr v-if="IsNotZero(asset.quantityHeld)">
            <td class="itemstyle">{{ asset.symbol }}</td>
            <td class="itemstyle">{{ asset.companyName }}</td>
            <td class="itemstyle">{{ asset.currentPrice }}</td>
            <td class="itemstyle">{{ asset.quantityHeld }}</td>
            <td class="itemstyle">${{ TotalEquity(asset).toFixed(2) }}</td>
            <!-- <td>{{- purchased price -}}</td> -->
            <td><button v-on:click ="PromptForPurchase(asset)" v-show=IsDollar(asset.symbol)>Buy Stock</button> <button v-on:click ="PromptForSale(asset)" v-show=IsDollar(asset.symbol)>Sell Stock</button></td>
          </tr>
        </tbody>
      </table>
      <label id="end7" class="tablefoot"></label>
    </div>
  </div>

</template>

<script>
import assetService from "../services/AssetService.js";
export default {
  data() {
    return {
        transactionRequest:{
            portfolioId:"",
            quantityAdjustment:"",
            USDAdjustment:"",
            assetId:""
        },
        
    };
  },
  methods: {
    IsNotZero(quantity){
      return (quantity>0)
    },
      IsDollar(symbol){
          return (symbol!='USD')
      },
    SetUserAssets() {
      assetService.getUserAssets(this.$store.state.user.userId,this.$store.state.selectedGame.gameId)
        .then((response) => {
          this.$store.commit("SET_ALL_STOCKS", response.data[0]);
          this.$store.commit("SET_SELECTED_ASSETS", response.data[1]);
        });
    },
    TotalEquity(asset) {
      let assetEquity = asset.quantityHeld * asset.currentPrice;
      this.netWorth += assetEquity;
      return assetEquity;
    },
    NetWorth() {
      let returnWorth = 0;
      this.$store.state.activeAssets.forEach((element) => {
        returnWorth += element.quantityHeld * element.currentPrice;
      });
      return returnWorth.toFixed(2);
    },
    PromptForPurchase(stock) {
      this.quantityToBuy = prompt("How many would you like to purchase?", "");
      if (stock.currentPrice * this.quantityToBuy <= this.$store.state.activeAssets[0].quantityHeld){
        this.transactionRequest.portfolioId = this.$store.state.activeAssets[0].portfolioID;
        this.transactionRequest.quantityAdjustment = this.quantityToBuy;
        this.transactionRequest.USDAdjustment = stock.currentPrice * this.quantityToBuy * -1;
        this.transactionRequest.assetId = stock.assetId;

        assetService.buyExistingStocks(this.$store.state.user.userId, this.$store.state.selectedGame.gameId, this.transactionRequest)
        
        .then((response) =>
            this.$store.commit("SET_SELECTED_ASSETS", response.data)
          )
        
        
      }
      else{
          alert("Insufficient funds, try liquidating some assets")
      }
    },
    PromptForSale(stock) {
      this.quantityToSell = prompt("How many would you like to sell?", "");
      if (stock.quantityHeld >= this.quantityToSell) {
        this.transactionRequest.portfolioId = this.$store.state.activeAssets[0].portfolioID;
        this.transactionRequest.quantityAdjustment = (this.quantityToSell*-1);
        this.transactionRequest.USDAdjustment = (stock.currentPrice * this.quantityToSell);
        this.transactionRequest.assetId = stock.assetId;
        assetService.buyExistingStocks(this.$store.state.user.userId, this.$store.state.selectedGame.gameId, this.transactionRequest)
        .then(response=>         
            this.$store.commit("SET_SELECTED_ASSETS", response.data)
          )
        
        
      }
      else{
          alert("You don't have that many, please try again")
      }
    },
  },
  computed: {},
  created() {
    this.SetUserAssets();
    // this.$store.state.user.userId, this.$store.state.selectedGame.gameId
  },
};
</script>

<style>

#portfoliocontainer{
    display:grid;
    grid-auto-columns: 1fr 8fr 1fr;
    grid-template-areas:
    ". thead ."
    ". tbody ."
    ". thead2 ."
    ". tbody2 ."
    ". foot .";

}

#leaderboardhead{
    grid-area: thead;
}

#portfoliobody{
    grid-area: tbody;
}

#stockshead{
    grid-area: thead2;
    border-top-left-radius: 0px;
    border-top-right-radius: 0px;
}

#stocksbody{
    grid-area: tbody2;
}

#end7{
    grid-area: foot;
}

</style>
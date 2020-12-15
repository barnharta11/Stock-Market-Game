<template>
  <div class="mainbackground">
    <div id="stockunicontainer">
      <h2 id="stockunihead" class="tableheader1">Stock Universe Menu</h2>
      <input class="textclass"
        id="stocksearch"
        type="text"
        v-model="currentSearch"
        placeholder="Search Stock Name..."
      />
      <table id="stockbody" class="tablebody">
        <thead>
          <tr>
            <th class="columnheader">Symbol</th>
            <th class="columnheader">Company Name</th>
            <th class="columnheader">Current Price</th>
            <th class="columnheader">Buy</th>
            <th class="columnheaderend">Last Purchase</th>
          </tr>
        </thead>
        <tbody v-for="stock in arrayByPage" v-bind:key="stock.assetId">
          <!-- <tbody> -->
          <tr>
            <td class="itemstyle">{{ stock.symbol }}</td>
            <td class="itemstyle">{{ stock.companyName }}</td>
            <td class="itemstyle">{{ stock.currentPrice }}</td>
            <td class="itemstyle">
              <button class="buttondefault" v-on:click="PromptForPurchase(stock)">Purchase</button>
            </td>
          </tr>
        </tbody>
      </table>
      <label id="end8" class="tablefoot"></label>
      <button id="backpage" class="buttondefault" v-on:click="DecreaseIndex()" v-show="showPrevious">
        Previous Page
      </button>
      <button id="nextpage" class="buttondefault" v-on:click="IncrementIndex()" v-show="showNext">
        Next Page
      </button>
    </div>
  </div>
</template>

<script>
import assetService from "../services/AssetService";
export default {
  data() {
    return {
      currentSearch: "",
      popupActive: false,
      quantityToBuy: "",
      transactionRequest: {
        portfolioId: "",
        quantityAdjustment: "",
        USDAdjustment: "",
        assetId: "",
      },
      existed: false,
      startIndex: 0,
      endIndex: 10,
    };
  },
  methods: {
    ToggleActive() {
      this.popupActive = !this.popupActive;
    },
    PromptForPurchase(stock) {
      this.quantityToBuy = prompt("How many would you like to purchase?", "");
      if (
        stock.currentPrice * this.quantityToBuy <=
        this.$store.state.activeAssets[0].quantityHeld
      ) {
        this.transactionRequest.portfolioId = this.$store.state.activeAssets[0].portfolioID;
        this.transactionRequest.quantityAdjustment = this.quantityToBuy;
        this.transactionRequest.USDAdjustment =
          stock.currentPrice * this.quantityToBuy * -1;
        this.transactionRequest.assetId = stock.assetId;

        this.$store.state.activeAssets.forEach((asset) => {
          if (asset.assetId == stock.assetId) {
            assetService.buyExistingStocks(
              this.$store.state.user.userId,
              this.$store.state.selectedGame.gameId,
              this.transactionRequest
            );
            this.existed = true;
          }
        });
        if (this.existed == false) {
          assetService.buyNewStocks(
            this.$store.state.user.userId,
            this.$store.state.selectedGame.gameId,
            this.transactionRequest
          );
        }
        assetService
          .getUserAssets(
            this.$store.state.user.userId,
            this.$store.state.selectedGame.gameId
          )
          .then((response) =>
            this.$store.commit("SET_SELECTED_ASSETS", response.data[1])
          );
      }
        else{
            alert("Insufficient funds, try liquidating some assets")
            }
    },
    IncrementIndex() {
      this.startIndex += 10;
      this.endIndex += 10;
    },
    DecreaseIndex() {
      this.startIndex -= 10;
      this.endIndex -= 10;
    },
  },
  computed: {
    searchedStocks() {
      let filtered = this.$store.state.allStocks.slice(1);
      if (this.currentSearch != "") {
        filtered = filtered.filter((stock) =>
          stock.companyName
            .toLowerCase()
            .includes(this.currentSearch.toLowerCase())
        );
      }
      return filtered;
    },
    arrayByPage() {
      return this.searchedStocks.slice(this.startIndex, this.endIndex);
    },
    showPrevious() {
      return this.startIndex > 0;
    },
    showNext() {
      return this.endIndex < this.searchedStocks.length - 1;
    },
  },
};
</script>

<style>

#stockunicontainer {
  display: grid;
  grid-template-columns: 1fr 4fr  4fr 1fr;
  grid-template-areas:
    ". thead thead ."
    ". search search ."
    ". tbody tbody ."
    ". foot foot ."
    ". back next .";
}

#stockunihead {
  grid-area: thead;
}


#stockunihead {
  grid-area: thead;
}

#stocksearch {
  grid-area: search;
}

#stockbody {
  grid-area: tbody;
}
#end8 {
  grid-area: foot;
}

#backpage {
  grid-area: back;
}

#nextpage {
  grid-area: next;
}
</style>
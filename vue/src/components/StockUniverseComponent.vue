<template>
  <div class="mainbackground">
    <div id="contain">
        <h2>Stock Universe Menu</h2>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Symbol</th>
                    <th>Company Name</th>
                    <th>Current Price</th>
                    <th>Buy</th>
                </tr>
            </thead>
            <tbody v-for="stock in $store.state.allStocks" v-bind:key="stock.assetId">
                <!-- <tbody> -->
                <tr>
                    <td>{{symbol}}</td>
                    <td>{{companyName}}</td>
                    <td>{{currentPrice}}</td>
                    <td>{{buttonToBuy}}</td>
                </tr>
            </tbody>
        </table>
    </div>  
</div>
</template>

<script>
import assetService from '../services/AssetService.vue'
export default {
    methods:{
        getAllStocks(){
            assetService.getAllStocks
            .then( result =>{
                this.$store.commit('SET_ALL_STOCKS', result.data)
            }
            )
        }
    },
    created(){
        this.getAllStocks()
    }

}
</script>

<style>

</style>
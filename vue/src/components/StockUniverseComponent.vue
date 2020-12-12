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
                    <td>{{stock.symbol}}</td>
                    <td>{{stock.companyName}}</td>
                    <td>{{stock.currentPrice}}</td>
                    <td>{{stock.buttonToBuy}}</td>
                </tr>
            </tbody>
        </table>
    </div>  
</div>
</template>

<script>

export default {

   

}
</script>

<style>

</style>
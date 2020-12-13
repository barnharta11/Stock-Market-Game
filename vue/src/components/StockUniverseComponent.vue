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
                </tr>
            </thead>
            <tbody v-for="stock in searchedStocks" v-bind:key="stock.assetId">
                <!-- <tbody> -->
                <tr>
                    <td>{{stock.symbol}}</td>
                    <td>{{stock.companyName}}</td>
                    <td>{{stock.currentPrice}}</td>
                    <td>
                   
                                test text
                        
                    </td>
                </tr>
            </tbody>
        </table>
    </div>  
</div>
</template>

<script>

export default {
    data(){
        return{
            currentSearch:"",
            popupActive:false
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
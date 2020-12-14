<template>
  <div class="mainbackground">
    <div id="" >
        <h2 id="leaderboardhead" class="tableheader1">Leaderboard</h2>
        <table id="leaderboardbody" class="tablebody">
            <thead>
                <tr>
                    <th class="columnheader">Standing</th>
                    <th class="columnheader">Username</th>
                    <th class="columnheader">Net Worth</th>
                    <th class="columnheaderend">Game Ends</th>
                </tr>
            </thead>
            <tbody v-for="player in sortedBoard" v-bind:key="player.userID">
                <tr>
                    <td class="itemstyle">{{PlayerStanding(player)}}</td>
                    <td class="itemstyle">{{player.userName}}</td>
                    <td class="itemstyle">{{player.netWorth}}</td>
                    <td class="itemstyleend">{{$store.state.selectedGame.endDate}}</td>
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
            sortedBoard:[]
        }
    },
    methods:{
        SortBoard(){
            this.sortedBoard=this.$store.state.currentGameLeaderBoard
            // this.sortedBoard.forEach(player=>{

            // })
        },
        PlayerStanding(player){
            return (this.sortedBoard.indexOf(player))+1
        }
    },
    created(){
        this.SortBoard()
    }
}
</script>

<style>



</style>
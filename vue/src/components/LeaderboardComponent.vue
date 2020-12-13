<template>
  <div class="mainbackground">
    <div id="" >
        <h2>Leaderboard</h2>
        <table id="gamelistcreatorname" class="smalltextclass">
            <thead>
                <tr>
                    <th>Standing</th>
                    <th>Username</th>
                    <th>Net Worth</th>
                    <th>Game Ends</th>
                </tr>
            </thead>
            <tbody v-for="player in sortedBoard" v-bind:key="player.userID">
                <tr>
                    <td>{{PlayerStanding(player)}}</td>
                    <td>{{player.userName}}</td>
                    <td>{{player.netWorth}}</td>
                    <td>{{$store.state.selectedGame.endDate}}</td>
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
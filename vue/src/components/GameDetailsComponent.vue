<template>
  <div id="gamedetailscontainer">
          <table>
            <thead id="gamedetailshead">
              <th>Game Name</th>
              <th>Game Creator</th>
              <th>Start Date</th>
              <th>End Date</th>
            </thead>
            <tbody id="gamedetailsbody">
              <tr>
                <td>{{selectedGame.gameName}}</td>
                <td>{{selectedGame.creatorName}}</td>
                <td>{{selectedGame.startDate}}</td>
                <td>{{selectedGame.endDate}}</td>
              </tr>
            </tbody>
          </table>
  </div>
</template>

<script>

export default {
 
  data(){
    return{
      selectedGame:[]
    }    
  },
  created(){
    this.selectedGame=this.$store.state.selectedGame
  }
  }


</script>

<style>
#gamedetailscontainer{
  display: grid;
  grid-template-columns: 1fr 1fr 1fr 1fr;
  grid-template-areas:
  "head head head head"
  "data data data data";
  width: 100vw;
}
.gamedetailshead{
  grid-area: head;
}
.gamedetailsbody{
  grid-area: data;
}

</style>
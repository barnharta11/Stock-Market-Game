<template>
  <div id="gamedetailscontainer">
    <h2 id="gamedetailshead" class="tableheader">Details</h2>
    <table id="gamedetailsbody" class="tablebody">
      <thead>
        <tr>
          <th class="columnheader">Game Name</th>
          <th class="columnheader">Game Creator</th>
          <th class="columnheader">Start Date</th>
          <th class="columnheaderend">End Date</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td class="itemstyle">{{ selectedGame.gameName }}</td>
          <td class="itemstyle">{{ selectedGame.creatorName }}</td>
          <td class="itemstyle">{{ selectedGame.formattedStartDate }}</td>
          <td class="itemstyleend">{{ selectedGame.formattedEndDate }}</td>
        </tr>
      </tbody>
    </table>
    <label id="end6" class="tablefoot"></label>
  </div>
</template>

<script>
export default {
  data() {
    return {
      selectedGame: [],
    };
  },
  created() {
    this.selectedGame = this.$store.state.selectedGame;
  },
};
</script>

<style>
#gamedetailscontainer {
  display: grid;
  grid-template-columns: 1fr 8fr 1fr;
  grid-template-areas:
    ". head ."
    ". data ."
    ". footer .";
}
#gamedetailshead {
  grid-area: head;
}
#gamedetailsbody {
  grid-area: data;
}

#end6{
  grid-area: footer;
}
</style>
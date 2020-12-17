<template>
  <div id="gamecard">
    <div id="title2" class="midtableheader">{{ Game.gameName }}</div>
    <table id="cardcomp" class="tablebody">
      <thead id="cardhead" class="carbackground">
        <tr id="headerrow">
          <th id="user" class="columnheader">username</th>
          <th id="net" class="columnheaderend">networth</th>
        </tr>
      </thead>
      <tbody
        id="info"
        v-for="board in Game.leaderboardList"
        v-bind:key="board.userGameID"
      >
        <tr id="cardbody">
          <td id="uname" class="itemstyle">{{ board.userName }}</td>
          <td id="networth" class="itemstyleend">{{ board.netWorth }}</td>
        </tr>
      </tbody>
    </table>
    <button
      id="detailbutton"
      class="textclass"
      v-on:click="SetSelectedGame(Game)"
    >
      Details
    </button>
  </div>
</template>

<script>
export default {
  props: {
    Game: Object,
  },
  data() {
    return {
      currentLeaderboardList: [],
    };
  },
  methods: {
    SetCurrentBoard(game) {
      this.currentLeaderboardList = game;
    },
    SetSelectedGame(game) {
      this.$store.commit("SET_SELECTED_GAME", game);
      this.$router.push(
        `/games/leaderboard/${this.$store.state.selectedGame.gameId}`
      );
    },
  },

  created() {
    this.SetCurrentBoard();
  },
};
</script>

<style>


#cardhead{
  border-top-left-radius: 0px;
}

#cardbody{
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas:
  "uname newtworth";
}

#headerrow {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-template-areas: "usergrid netgrid";
}

#user {
  grid-area: usergrid;
}

#net {
  grid-area: netgrid;
}


#cardcomp {
  display: grid;
  grid-area: comp;
  grid-template-columns: 1fr;
  grid-template-areas:
    "head"
    "info";
}


#title2 {
  text-align: center;
  grid-area: title;
}

#gamecard {
  margin-top: 0px;
  display: grid;
  grid-template-columns: 1fr;
  grid-template-areas:
    "title"
    "comp"
    "btn";
}

#detailbutton {
  padding-top: 0;
  text-align: center;
  /* line-height: 40px; */
  border-left: none;
  border-right: none;
  border-top: rgb(105, 172, 105) 2px solid;
  font-size: larger;
  grid-area: btn;
}

</style>
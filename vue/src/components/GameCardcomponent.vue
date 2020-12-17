<template>
  <div id="gamecard" class="game">
    <div id="title" class="tableheader">{{ Game.gameName }}</div>
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
        <tr>
          <td>{{ board.userName }}</td>
          <td>{{ board.netWorth }}</td>
        </tr>
      </tbody>
    </table>
      Game Details
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

#cardhead {
}

#cardcomp {
  display: grid;
  grid-area: comp;
  grid-template-columns: 1fr;
  grid-template-areas:
    "head"
    "info";
}

#detailbutton {
  grid-area: btn;
}

#name {
  grid-area: name;
}

#networth {
  grid-area: worth;
}

#title {
  text-align: center;
  grid-area: title;
}

#gamecard {
  margin-top: 15px;
  display: grid;
  grid-template-columns: 1fr;
  grid-template-areas:
    "title"
    "comp"
    "btn";
}
</style>
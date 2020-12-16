<template>
  <div id="gamecard" class="game">
    <div id="title" class="header">{{ Game.gameName }}</div>
    <table
      id="info"
      v-for="board in Game.leaderboardList"
      v-bind:key="board.userGameID"
    >
    <thead>
      <tr>
        <th> username</th>
        <th> networth</th>
      </tr>
    </thead>
</table>
<table>
      <tbody>
        <tr>
          <td>{{ board.userName }}</td>
          <td>{{ board.netWorth }}</td>
        </tr>
      </tbody>
    </table>
    <button id="detailbutton" v-on:click="SetSelectedGame(Game)">
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
/* #detailbutton {
  grid-area: btn;
}

#info {
  display: grid;
  grid-area: info;
  grid-template-columns: 1fr;
  grid-template-areas:
    "name"
    "worth";
}
#name {
  grid-area: name;
}

#networth {
  grid-area: worth;
}

#title {
  grid-area: title;
}

#gamecard {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:
    ". title ."
    ". info ."
    ". btn .";
} */
</style>
<template>
  <div class="game">
    <div class="game-title">{{ Game.gameName }}</div>
    <div v-for="player in Game.leaderboardList" v-bind:key="player.userGameId">
      <div>{{ player.userName }}</div>
    </div>
    <router-link
    :to="`/assets/${this.$store.state.user.userId}/${player.gameID}`">game details</router-link>
  </div>
</template>

<script>
export default {
  props: {
    Game: Object,
  },
};
</script>

<style>
</style>
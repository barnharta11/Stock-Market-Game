<template>
  <div>
    <h1>Current Games</h1>
    <users-games-component/>
    <h1>All Games</h1>
    <game-list-component/>
    <h1>Pending Invites</h1>
    <users-pending-component/>
  </div>
</template>

<script>
import GameListComponent from '../components/GameListComponent.vue'
import UsersGamesComponent from '../components/UsersGamesComponent.vue'
import UsersPendingComponent from '../components/UsersPendingComponent.vue'
export default {
  components: { GameListComponent, UsersGamesComponent, UsersPendingComponent },

}
</script>

<style>

</style>
<template>
<div class="mainbackground">
  <div>
    <!-- <div id="mycurrentgamesheader" class="tableheader"> My Current Games</div> -->
    <users-games-component/>
    <!-- <div id="allgamesheader" class="tableheader">All Games</div> -->
    <game-list-component/>
    <!-- <div id="mypendinginvitesheader" class="tableheader"> My Pending Invites</div> -->
    <users-pending-component/>
  </div>
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

#bigbackground{
    background-color: rgb(105,172,105);
    height: 500vh;
}

#mycurrentgamesheader{
  margin-right: 10%;
  margin-left: 20%;
}

#allgamesheader{
  margin-right: 10%;
  margin-left: 20%;
}

#mypendinginvitesheader{
  margin-right: 10%;
  margin-left: 20%;
}

</style>
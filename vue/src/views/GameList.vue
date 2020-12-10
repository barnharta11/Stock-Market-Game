<template>
<div class="bigbackground">
  <div >
    <div id="mycurrentgamesheader" class="tableheader"> My Current Games</div>
    <users-games-component/>
    <div id="allgamesheader" class="tableheader">All Games</div>
    <game-list-component/>
    <div id="mypendinginvitesheader" class="tableheader"> My Pending Invites</div>
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
  /* border-bottom-left-radius: 25px; */
}

#allgamesheader{
  margin-right: 10%;
  margin-left: 20%;
}

#mypendinginvitesheader{
  margin-right: 10%;
  margin-left: 20%;
}

.tableheader{
  font-family: Consolas, Arial, Helvetica;
  border-top-right-radius: 25px;
  border-top-left-radius: 25px;
  font-size: 50px;
  text-align: center;
  color: rgb(105, 172, 105);
  font-weight: bolder;
  line-height: 50px;
  background-color: lightgray;
  margin-top: 10px;
  padding-bottom: 25px;
  padding-top: 15px;
}
</style>
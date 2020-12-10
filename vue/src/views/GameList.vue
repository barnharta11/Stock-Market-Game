<template>
<div class="mainbackground">
  <div>
    <div class="tableheader"> My Current Games</div>
    <users-games-component/>
    <div class="tableheader">All Games</div>
    <game-list-component/>
    <div class="tableheader"> My Pending Invites</div>
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
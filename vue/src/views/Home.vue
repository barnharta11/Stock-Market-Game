<template>
  <div class="mainbackground">
    <div id="home" class="mainbackground">
      <div id="homelabel">
        <h1 id="homelabel2" class="label">Welcome {{this.$store.state.user.username}}!</h1>
      </div>
      <div id="nav2">
        <h1 id="navheader" class="tableheader">Nav</h1>
        <router-link id="creategamelink" :to="{ name: 'creategame' }"
          >Create Game</router-link
        >
        <router-link id="allgameslink" :to="{ name: 'allgames' }"
          >List All Games</router-link
        >
        <router-link
          id="createdgameslink"
          :to="{
            name: 'createdgames',
            params: { userid: this.$store.state.user.userId },
          }"
          >List Games You Created</router-link
        >
        <router-link
          id="pendinglink"
          :to="{
            name: 'pendinginvites',
            params: { userid: this.$store.state.user.userId },
          }"
          >Pending Invites</router-link
        >
        <label id="mainfooter" class="tablefooter"></label>
      </div>
      <label id="activityheader" class="tableheader">Current Activity</label>
      <div id="leaderboard">
    <users-games-component/>
      </div>
    </div>
  </div>
</template>

<script>
import UsersGamesComponent from '../components/UsersGamesComponent.vue'
// import LeaderboardComponent from "../components/LeaderboardComponent.vue";
export default {
  name: "home",
  components: { UsersGamesComponent },
};
</script>

<style>

#activityheader{
  grid-area: ah;
}

#leaderboard {
  grid-area: leaderboardtable;
}

#homelabel {
  grid-area: home;
}

#home {
  display: grid;
  grid-template-columns: 0.5fr 3fr 0.5fr 2fr 0.5fr;
  grid-template-areas:
  ". home . gridnav ."
  ". ah ah ah ."
  ". leaderboardtable leaderboardtable leaderboardtable .";
}

#navheader {
  grid-area: navhead;
}

#nav2 {
  margin-bottom: 20px;
  grid-area: gridnav;
  display: grid;
  grid-template-columns: 1fr;
  grid-template-areas:
    "navhead"
    "create"
    "allgames"
    "created"
    "pending"
    "footer";
}

#leaderboardcomp {
  grid-area: leaderboard;
}

#creategamelink:link {
  grid-area: create;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#creategamelink:visited {
  grid-area: create;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#creategamelink:hover {
  grid-area: create;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: black;
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#allgameslink:link {
  grid-area: allgames;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#allgameslink:visited {
  grid-area: allgames;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#allgameslink:hover {
  grid-area: allgames;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: black;
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#createdgameslink:link {
  grid-area: created;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#createdgameslink:visited {
  grid-area: created;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#createdgameslink:hover {
  grid-area: created;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: black;
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}


#pendinglink:link{
  grid-area: pending;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#pendinglink:visited{
  grid-area: pending;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}
#pendinglink:hover{
  grid-area: pending;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  font-size: 35px;
  text-align: center;
  color: black;
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}


#mainfooter {
  grid-area: footer;
}
</style>


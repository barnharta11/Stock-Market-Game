<template>
  <div class="mainbackground">
    <div id="home">
      <div id="homelabel">
        <h1 id="homelabel2" class="label">Home</h1>
        <!-- <div>
      <leaderboard-component />
      </div> -->
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
          :to="{ name: 'createdgames',
            params: { userid: this.$store.state.user.userId },
          }"
          >List Games You Created</router-link
        >
        <router-link
          id="pendinglink"
          :to="{ name: 'pendinginvites',
            params: { userid: this.$store.state.user.userId },
          }"
          >Pending Invites</router-link
        >
        <label id="mainfooter" class="tablefooter"></label>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
};
</script>

<style>


#homelabel {
  grid-area: home;
}

#home {
  display: grid;
  grid-template-columns: 0.5fr 3fr 0.5fr 2fr 0.5fr;
  grid-template-areas: ". home . gridnav .";
}

#navheader {
  grid-area: navhead;
  margin-bottom: 0;
}

#nav2{
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

#creategamelink {
  grid-area: create;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  /* padding-left: 19px; */
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#allgameslink {
  grid-area: allgames;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  /* padding-left: 19px; */
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#createdgameslink {
  grid-area: created;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  /* padding-left: 19px; */
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#pendinglink{
  grid-area: pending;
    font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  /* padding-left: 19px; */
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;

}


#mainfooter {
  grid-area: footer;
}
</style>


<template>
  <div class="mainbackground">
    <div class="home">
      <h1 id="homeheader" class="tableheader">Home</h1>
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
      <label id="mainfooter" class="tablefooter"></label>
    </div>
  </div>
</template>

<script>
export default {
  name: "home",
};
</script>

<style>

.home {
  display: grid;
  grid-template-columns: 1fr 2fr 1fr;
  grid-template-areas:
    ". head ."
    ". create ."
    ". allgames ."
    ". created ."
    ". footer .";
}

#homeheader {
  grid-area: head;
  margin-bottom: 0;
}

#creategamelink {
  grid-area: create;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 19px;
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
  padding-left: 19px;
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
  padding-left: 19px;
  font-size: 35px;
  text-align: center;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#mainfooter{
  grid-area: footer;
}
</style>


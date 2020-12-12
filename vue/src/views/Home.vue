<template>
<div id="mainhome">
  <div class="home">
    <h1 id="homeheader">Home</h1>
    <router-link id="creategamelink" :to="{ name: 'creategame'}">Create Game</router-link>
    <router-link id="allgameslink" :to="{name:'allgames'}">List All Games</router-link>
    <router-link id="createdgameslink" :to="{name:'createdgames', params: {userid:this.$store.state.user.userId}}">List Games You Created</router-link>
    <label id="end" class="textclass"></label>

  </div>
  </div>
</template>

<script>
export default {
  name: "home"
}
</script>

<style>

#mainhome{
  height: 100vh;
  background-color: rgb(105,172,105);

}

.home{
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-template-areas:
  ". head ."
  ". create ."
  ". allgames ."
  ". created ."
  ". formbottom .";
}

#homeheader{
  grid-area: head;
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
  margin-bottom: 0px;
  padding-bottom: 25px;
  padding-top: 15px;
}

#creategamelink{
  grid-area: create;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 19px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#allgameslink{
  grid-area: allgames;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 19px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#createdgameslink{
  grid-area: created;
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 19px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  text-decoration: none;
}

#end{
  grid-area: footer;
  border-bottom-right-radius: 25px;
  border-bottom-left-radius: 25px;
  padding-top: 35px;
  padding-bottom: 3px;
  padding-left: 4px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
  grid-area: formbottom;
  bottom: 0%;

}


</style>


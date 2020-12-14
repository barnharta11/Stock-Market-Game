<template>
  <body>
    <div id="app">
      <div id="nav">
        <router-link id="homebutton" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;&nbsp;
        <span id="seperator">|</span>
        <router-link id="logoutbutton" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
        <span id="title">Poor Inve$tment of Time</span>
      </div>
      <router-view />
    </div>
  </body>
</template>

<style>
body {
  margin: 0px;
}

#title{
  grid-area: title;
  text-align: right;
}

#seperator{
  grid-area: sep;
  text-align: center;
}

#app {
  background-color: rgb(105, 172, 105);
}

#nav {
  display: grid;
  background-color: black;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  grid-template-columns:  1fr 1fr 1fr 5fr 5fr;
  grid-template-areas: 
  "homebtn sep logoutbtn . title";
  padding-bottom: 0;
  margin: 0;
}
#homebutton:link {
  grid-area: homebtn;
  background-color: black;
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#homebutton:visited {
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#homebutton:hover {
  color: white;
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#logoutbutton:link {
  grid-area: logoutbtn;
  background-color: black;
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#logoutbutton:visited {
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#logoutbutton:hover {
  color: white;
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

.mainbackground {
  font-family: Consolas, Arial, Helvetica;
  background-color: rgb(105, 172, 105);
  height: 100vh;
}

.tableheader {
  border-top-right-radius: 25px;
  border-top-left-radius: 25px;
  color: rgb(105, 172, 105);
  background-color: black;
  font-family: Consolas, Arial, Helvetica;
  font-weight: bolder;
  font-size: 50px;
  text-align: center;
  padding-bottom: 15px;
  padding-top: 10px;
}

.tablefooter {
  border-bottom-right-radius: 25px;
  border-bottom-left-radius: 25px;
  background-color: lightgray;
  padding-top: 25px;
  font-size: 35px;
  line-height: 30px;
}

.formclass{
  font-family: Consolas, Arial, Helvetica;
  border-color: gray;
  font-size: 28px;
  color: rgb(13, 42, 13);
  background-color: lightgray;
  line-height: 20px;
  padding-left: 15px;
}

.textclass{
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 19px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
}

</style>

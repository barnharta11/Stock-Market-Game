<template>
<body>
  <div id="app">
    <div id="nav">
      <router-link id="homebutton" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;&nbsp;|
      <router-link id="logoutbutton" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
  </div>
</body>
</template>

<style>

body{
  margin: 0px;
}

#app{
  background-color: rgb(105, 172, 105);
  margin: 0px;
}

#nav{
  background-color: black; 
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
}
#homebutton:link{
  background-color: black;
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#homebutton:visited{
  color:rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#homebutton:hover{
  color: white;
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#logoutbutton:link{
  background-color: black;
  color: rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#logoutbutton:visited{
  color:rgb(105, 172, 105);
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

#logoutbutton:hover{
  color: white;
  font-family: Consolas, Arial, Helvetica;
  font-size: 35px;
  text-decoration: none;
}

.mainbackground{
    font-family: Consolas, Arial, Helvetica;
    padding-top: 25px;
    background-color: rgb(105,172,105);
    height: 100vh;
}



</style>

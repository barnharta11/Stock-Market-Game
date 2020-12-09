<template>
  <div id="app">
    <div id="nav">
      <router-link id="homebutton" v-bind:to="{ name: 'home' }">Home</router-link>&nbsp; &nbsp;
      <router-link id="logout" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>
#app{
  background-color: black; 
  font-family: Consolas, Arial, Helvetica;
}
#homebutton:link{
  color: rgb(105, 172, 105);
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


</style>

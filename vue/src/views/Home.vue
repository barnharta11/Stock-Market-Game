<template>
  <div class="home">
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
    <div><router-link :to="{ name: 'creategame'}">Create Game</router-link></div>
    <div><router-link :to="{name:'allgames'}">List All Games</router-link></div>
    <div><router-link :to="{name:'createdgames', params: {userid:this.$store.state.user.userId}}">List Your Games</router-link></div>
  </div>
</template>

<script>
export default {
  name: "home"
};
</script>

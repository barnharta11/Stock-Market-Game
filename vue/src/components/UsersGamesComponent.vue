<template>
  <div>
      <div id="usersgamescontainer" v-for="game in this.$store.state.userGames" v-bind:key="game.id">
                    <!-- <router-link :to="{name:'gamedetails', params: {gameid:game.gameId}}"> -->
          <button class="buttondefault" v-on:click='SetSelectedGame(game)'>
            <span>{{game.gameName}} </span>
          </button>
              <!-- </router-link> -->
          <!-- <div> -->
            <span id="currentname" class="textclass">{{game.creatorName}}</span>
            <span id="currentstart" class="textclass">{{game.startDate}}</span>
            <span id="currentend" class="textclass">{{game.endDate}}</span>
            <span id="currentstatus" class="textclass">{{game.statusName}}</span>
          <!-- </div> -->
          <!-- <span> -->
              <!-- <router-link :to -->
          <!-- </span> -->
      </div>
  </div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push({name:'gamedetails', params: {gameid:game.gameId} })
    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            this.$store.commit("SET_USERSGAMES", response.data)
        })
    }
},
created(){
    this.GetUsersGames()

}
}
</script>

<style>


#usersgamescontainer{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 2.5fr 2.5fr 1fr 1fr;
    grid-template-areas: 
    ". buttonslot name start end status .";
    /* padding: 100px; */
}

.buttondefault{
  grid-area: buttonslot;
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  /* padding: 10px 20px; */
  font-size: 20px;
  /* line-height: 20px; */
  width: 80%;
  margin: 10px;
  /* margin-bottom: 10px ; */
  transition-duration: .4s;
}

#currentname{
    grid-area: name;
        border-right: 2px solid rgb(105, 172, 105);

}

#currentstart{
    grid-area: start;
    border-right: 2px solid rgb(105, 172, 105);
}

#currentend{
    grid-area: end;
    border-right: 2px solid rgb(105, 172, 105);
}

#currentstatus{
    grid-area: status;
}

</style>
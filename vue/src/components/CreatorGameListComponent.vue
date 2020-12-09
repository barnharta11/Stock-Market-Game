<template>
  <div class="mainbackground">
      <div id="contain" v-for="game in creatorGames" v-bind:key="game.id">
          <router-link id="buttontogamedetails" class="gamebutton" :to="{name:'gamedetails', params: {gameid:game.gameId}}">
          <button class="buttontext" v-on:click='SetSelectedGame(game)'>
              {{game.gameName}}  
              </button>
              </router-link>
          <span id="gamelistcreatorname" class="smalltextclass">{{game.creatorName}} | </span>
          <span id="gameliststartdate" class="smalltextclass">{{game.startDate}} | </span>
          <span id="gamelistenddate" class="smalltextclass">{{game.endDate}} | </span>
          <span id="gameliststatus" class="smalltextclass">{{game.statusName}} | </span>
          <span id="gamelistid" class="smalltextclass">{{game.gameId}}</span>
          
      </div>
  </div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
    data(){
        return{
            creatorGames: [],

        }
    },
methods:{
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
    },
    GetUsersGames(){
        gameService.getUsersGames(this.$store.state.user.userId)
        .then(response=>{
            response.data.forEach(element => {
                if(element.creatorName==this.$store.state.user.username){
                    this.creatorGames.push(element)
                }
            });
        })
    }
},
created(){
    this.GetUsersGames()

}
}
</script>

<style>

#contain{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr;
    grid-gap:5px;
    grid-template-areas: 
    ". button creatorname startdate enddate status id .";
}

.mainbackground{
    background-color: rgb(105,172,105);
    height: 100vh;
}

.smalltextclass{
  font-family: Consolas, Arial, Helvetica;
  padding-top: 15px;
  padding-bottom: 3px;
  padding-left: 19px;
  font-size: 20px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
}


#gamelistcreatorname{
    grid-area: creatorname;
}

#gameliststartdate{
    grid-area: startdate;
}

#gamelistenddate{
    grid-area: enddate;
}

#gameliststatus{
    grid-area: status;
}

#gamelistid{
    grid-area: id;
}


/* .gamebutton{
grid-area: createbutton;
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  padding: 10px 20px;
  font-size: 20px;
  line-height: 20px;
  width: 50%;
  margin-top: 20px;
  margin-bottom: 100%;
  transition-duration: .4s;
} */

.buttontext{
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  padding: 10px 20px;
  font-size: 20px;
  line-height: 20px;
  width: 50%;
  margin-top: 20px;
  margin-bottom: 100%;
  transition-duration: .4s;
  grid-area: button;

}
</style>
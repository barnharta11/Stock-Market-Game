<template>
  <div class="mainbackground">
      <div id="contain" v-for="game in creatorGames" v-bind:key="game.id">
          <!-- <label id="top" class="tabletop"></label> -->

          <!-- <router-link id="buttontogamedetails" class="gamebutton" :to="{name:'gamedetails', params: {gameid:game.gameId}}"> -->
          <button id="button2" v-on:click='SetSelectedGame(game)'>
              {{game.gameName}}  
              </button>
              <!-- </router-link> -->
          <span id="gamelistcreatorname" class="smalltextclass">{{game.creatorName}} </span>
          <span id="gameliststartdate" class="smalltextclass">{{game.startDate}} </span>
          <span id="gamelistenddate" class="smalltextclass">{{game.endDate}} </span>
          <span id="gameliststatus" class="smalltextclass">{{game.statusName}} </span>
          <span id="gamelistid" class="smalltextclass">{{game.gameId}}</span>
<<<<<<< HEAD
          <!-- move here button that feeds selected game -->
=======
          <router-link v-on:click="SetInviteGame(game)" :to="{name:'displayusers'}">Invite users</router-link>
          <!-- <label id="end" class="formfooter"></label> -->

>>>>>>> ba4e35e74da023b8475edde0c4626cf125c7eafd
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
    SetInviteGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
    },
    SetSelectedGame(game){
        this.$store.commit("SET_SELECTED_GAME", game)
        this.$router.push({name:'gamedetails', params: {gameid:game.gameId} })
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
    grid-template-columns: 1fr 3fr 1fr 3fr 3fr 1fr .5fr 1fr;
    grid-template-areas: 
    /* ". . top top top top top top ." */
    ". button creatorname startdate enddate status id .";
}

/* #top{
    background-color: lightgray;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
    grid-area: top;
    
} */

.mainbackground{
    padding-top: 25px;
    background-color: rgb(105,172,105);
    height: 100vh;
}

.smalltextclass{
  font-family: Consolas, Arial, Helvetica;
    /* border-right: 3px, solid, rgb(105, 172, 105); */

  /* padding-top: 15px; */
  /* padding-bottom: 3px; */
  /* padding-left: 19px; */
  font-size: 20px;
  text-align: center;
  /* text-justify: auto; */
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;
}


#gamelistcreatorname{
    grid-area: creatorname;
    border-right: 2px solid rgb(105, 172, 105);
}

#gameliststartdate{
    grid-area: startdate;
    border-right: 2px solid rgb(105, 172, 105);

}

#gamelistenddate{
    grid-area: enddate;
    border-right: 2px solid rgb(105, 172, 105);

}

#gameliststatus{
    grid-area: status; 
    border-right: 2px solid rgb(105, 172, 105);

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

#button2{
  background-color: lightgray;
  font-family: Consolas, Arial, Helvetica;
  color: rgb(105, 172, 105);
  border: none;
  border-radius: 10px;
  /* padding: 10px 20px; */
  font-size: 20px;
  /* line-height: 20px; */
  /* width: 50%; */
  margin: 5px;
  /* margin-bottom: 100%; */
  transition-duration: .4s;
  grid-area: button;

}
</style>
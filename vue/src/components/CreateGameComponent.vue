<template>
<div id="creategamemain">
  <div id="create-game-form" class="text-center">
    <form class="form-create-game" @submit.prevent="createGame()">
      <h1 id="createheader">Create Game</h1>
      <label id="creategamename" for="gamename" class="textclass">Gamename</label>
      <input type="text" id="gamenamein" class="formclass" placeholder="Gamename..." v-model="formgame.gamename" required autofocus/>
      <label id="createstartdate" for="start-date" class="textclass">Start Date</label>
      <input type="date" id="startdatein" class="formclass" placeholder="Start Date..." v-model="formgame.startdate" required autofocus/>
      <label id="createenddate" for="end-date" class="textclass">End Date</label>
      <input type="date" id="enddatein" class="formclass" placeholder="End Date..." v-model="formgame.enddate" required/>
      <label id="createend" class="form"></label>
      <button id="createbutton" class="btn" type="submit">Create Game</button>
    </form>
  </div>
</div>
</template>

<script>
import gameService from "../services/GameService.js"
export default {
data(){
    return{
        formgame:{
            gamename:"",
            creatorname: "",
            startdate: "",
            enddate: ""
        }
    }
},
methods:{
    createGame(){
        this.formgame.creatorname = this.$store.state.user.username
        gameService.createGame(this.formgame)
        .then(response =>{
            if(response.status==201){
                this.$router.push('/')
            }
        })
    }
}
}
</script>

<style>

#creategamemain{
  background-color: rgb(105,172,105);
  height: 100vh;
  margin-top: 0px;
}

.form-create-game{
  display: grid;
  grid-template-columns: 1fr 2fr 2fr 1fr;
  grid-template-areas: 
  ". header header ."
  ". gamename gamename ."
  ". createin createin ."
  ". start start ."
  ". startin startin ."
  ". end end ."
  ". endin endin ."
  ". createfooter createfooter ."
  ". createbut . ."


  ;
}

#createheader{
  grid-area: header;
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

#creategamename{
  grid-area: gamename;
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

#gamenamein{
  grid-area: createin;
  font-family: Consolas, Arial, Helvetica;
  border-color: gray;
  font-size: 28px;
  color: rgb(13, 42, 13);
  background-color: lightgray;
  line-height: 20px;
  padding-left: 15px;

}

#createstartdate{
  grid-area: start;
}

#startdatein{
  grid-area: startin;
}

#createenddate{
  grid-area: end;
}

#enddatein{
  grid-area: endin;
}

#createend{
  grid-area: createfooter;  border-bottom-right-radius: 25px;
  border-bottom-left-radius: 25px;
  padding-top: 35px;
  padding-bottom: 3px;
  padding-left: 4px;
  font-size: 35px;
  text-align: left;
  color: rgb(105, 172, 105);
  line-height: 30px;
  background-color: lightgray;

}




#createbutton{
  grid-area: createbut;
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

}

</style>
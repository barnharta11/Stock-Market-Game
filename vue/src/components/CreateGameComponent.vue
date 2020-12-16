<template>
<div class="mainbackground">
  <div>
    <form id="creategamecontainer" @submit.prevent="createGame()">
      <h1 id="createheader" class="tableheader">Create Game</h1>
      <label id="creategamename" for="gamename" class="textclass">Gamename</label>
      <input type="text" id="gamenamein" class="formclass" placeholder="Gamename..." v-model="formgame.gamename" required autofocus/>
      <label id="createstartdate" for="start-date" class="textclass">Start Date</label>
      <input type="date" id="startdatein" class="formclass" placeholder="Start Date..." v-model="formgame.startdate" required autofocus/>
      <label id="createenddate" for="end-date" class="textclass">End Date</label>
      <input type="date" id="enddatein" class="formclass" placeholder="End Date..." v-model="formgame.enddate" required/>
      <label id="createfooter" class="tablefooter"></label>
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
                alert(`${this.formgame.gamename} has been created.`)
                this.$router.push(`/games/${this.$store.state.user.userId}/createdgames`)
            }
          })
         .catch(error=>{
           if(error.response.status==409){
               alert(`${this.formgame.gamename} has already been used, please try another.`)
             }
             else{
              alert('There has been an error, please contact support or try again at a later time.')
            }
          }
         )
                 
    }
}
}
</script>

<style>

#creategamecontainer{
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
  ". createbut . .";
}

#createheader{
  grid-area: header;
  margin: 0;
}

#creategamename{
  grid-area: gamename;
}

#gamenamein{
  grid-area: createin;
  font-family: Consolas, Arial, Helvetica;
  border-color: gray;
  color: rgb(0, 65, 0);
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

#createfooter{
  grid-area: createfooter;
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
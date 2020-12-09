<template>
<div id="creategamemain">
  <div id="create-game-form" class="text-center">
    <form class="form-create-game" @submit.prevent="createGame()">
      <h1 class="h3 mb-3 font-weight-normal">Create Game</h1>
      <label for="gamename" class="sr-only">Gamename</label>
      <input
        type="text"
        id="gamename"
        class="form-control"
        placeholder="Gamename"
        v-model="formgame.gamename"
        required
        autofocus
      />
      <label for="start-date" class="sr-only">Start Date</label>
      <input
        type="date"
        id="start-date"
        class="form-control"
        placeholder="Start Date"
        v-model="formgame.startdate"
        required
        autofocus
      />
      <label for="end-date" class="sr-only">End Date</label>
      <input
        type="date"
        id="end-date"
        class="form-control"
        placeholder="End Date"
        v-model="formgame.enddate"
        required
      />
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Game
      </button>
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
  
}
</style>
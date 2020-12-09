import axios from 'axios';

export default {
    createGame(game){
        return axios.post('/creategame', game)
    },
    listAllGames(){
        return axios.get('/games')
    },
    getUsersGames(id){
        return axios.get(`/games/${id}`)
    }
}
import axios from 'axios';

export default {
    getGameLeaderboard(gameId){
        return axios.get(`/games/leaderboard/${gameId}`)
    },

}
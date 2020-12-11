import axios from 'axios';

export default {
    getUserPortfolio(userId, gameId){
        return axios.get(`/portfolio/${userId}/${gameId}`)
    }
}
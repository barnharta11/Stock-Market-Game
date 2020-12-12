import axios from 'axios';

export default {
    getUserAssets(userId, gameId){
        return axios.get(`/assets/${userId}/${gameId}`)
    },
    getStocks(){
        return axios.get('/stocks')
    }
}
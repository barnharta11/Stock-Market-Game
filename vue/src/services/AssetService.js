import axios from 'axios';

export default {
    getUserAssets(userId, gameId){
        return axios.get(`/assets/${userId}/${gameId}`)
    },
    buyNewStocks(userId, gameId, transactionRequest){
        return axios.post(`/assets/${userId}/${gameId}`, transactionRequest)
    },
    buyExistingStocks(userId, gameId, transactionRequest){
        return axios.put(`/assets/${userId}/${gameId}`, transactionRequest)
    }
}
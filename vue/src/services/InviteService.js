import axios from 'axios';

export default {
    inviteUser(userId, gameId){
        return axios.get('/invite', userId, gameId)
    }
}
import axios from 'axios';

export default {
    inviteUser(invite){
        return axios.post('/inviteusers', invite)
    },
    acceptInvite(invite){
        return axios.put('/games', invite)
    }
}
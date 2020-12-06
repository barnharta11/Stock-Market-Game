import axios from 'axios';

export default {
    listAllUsers(){
        return axios.get('/user')
    }
}

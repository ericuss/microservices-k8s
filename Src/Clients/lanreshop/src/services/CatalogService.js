import axios from './CustomAxios'

export default {
    get: () => {
        return axios.get(`${process.env.VUE_APP_BFF_URL}/api/catalog`);
    }
}
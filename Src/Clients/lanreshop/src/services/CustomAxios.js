import axios from 'axios'

export function AddToken() {
    axios.interceptors.request
        .use(
            (config) => config,
            (err) => {
                if (process.env.NODE_ENV === 'development') {
                    // eslint-disable-line no-use-before-define
                    console.log('Axios error')
                    // eslint-disable-line no-use-before-define
                    console.log(err)
                }
                // What do we do when we get errors?
            })
}

export default axios
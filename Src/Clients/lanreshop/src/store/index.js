import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';


Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    products: [
      { id: 1, name: 'beer', stock: 11, src: 'https://cdn.vuetifyjs.com/images/cards/house.jpg' }
    ]
  },
  getters: {
    products: state => state.products,
    productsCount: (state, getters) => getters.products.length
  },
  mutations: {
    setProducts (state, products) {
      state.products = products || []
    },
    pushProduct (state, newProduct) {
      state.products.push(newProduct)
    },
    updateProduct (state, modifiedProduct) {
      let client = state.products.find(x => x.id == modifiedProduct.id)
      client.name = modifiedProduct.name
    },
    removeProduct (state, id) {
      state.products = state.products.find(x => x.id != id) || []
    }
  },
  actions: {
    async setNewProduct (context, newProduct) {
      context.commit('pushProduct', newProduct)
      return Promise.resolve()
    },
    async updateProduct (context, modifiedProduct) {
      context.commit('updateProduct', modifiedProduct)
      return Promise.resolve()
    },
    async removeProduct (context, id) {
      context.commit('removeProduct', id)
      return Promise.resolve()
    },
    async getProducts (context) {
      // context.commit('pushProduct',
      //   { id: 1, name: 'blablabla', stock: 11, src: 'https://cdn.vuetifyjs.com/images/cards/house.jpg' }
      // )
      // return Promise.resolve()
      return axios
        .get('/user?ID=12345')
        .then(data => {
          context.commit('setProducts', data)
        })
        // .catch(thrown => {
        //   if (axios.isCancel(thrown)) {
        //     // console.log('Request canceled', thrown.message);
        //   } else {
        //     // handle error
        //   }
        // })
        ;

      // return Promise.resolve()
    }
  },
  modules: {
  }
})

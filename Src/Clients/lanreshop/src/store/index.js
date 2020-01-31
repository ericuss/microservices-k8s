import Vue from 'vue'
import Vuex from 'vuex'

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
    }
  },
  modules: {
  }
})

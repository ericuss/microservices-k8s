import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/products',
    name: 'products',
    component: () => import(/* webpackChunkName: "about" */ '../views/Products.vue')
  },
  {
    path: '/',
    name: 'catalog',
    component: () => import(/* webpackChunkName: "about" */ '../views/Catalog.vue')
  }
]

const router = new VueRouter({
  routes
})

export default router

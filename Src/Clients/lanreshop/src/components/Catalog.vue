<template>
  <div class="catalog">
    <h3>{{name}}</h3>
    <div>
      <div v-for="product in products" v-bind:key="product.id">
        <CatalogProduct v-bind:product="product"></CatalogProduct>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import { mapGetters, mapActions } from "vuex";
import CatalogProduct from '@/components/CatalogProduct';

export default {
  computed: {
    ...mapGetters(["products", "productsCount"]),
  },
  components:{
    CatalogProduct
  },
  beforeMount() {
    this.getProducts()
  },
  data() {
    return {
      dialog: false,
      name: "catalog"
    };
  },
  methods:{
    ...mapActions(["getProducts"])
  }
};
</script>

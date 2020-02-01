<template>
  <div class="catalog">
    <h3>{{name}}</h3>
    <div class="products">
      <CatalogProduct
        class="product"
        v-for="product in products"
        v-bind:key="product.id"
        v-bind:product="product"
      ></CatalogProduct>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import { mapGetters, mapActions } from "vuex";
import CatalogProduct from "@/components/CatalogProduct";

export default {
  computed: {
    ...mapGetters(["products", "productsCount"])
  },
  components: {
    CatalogProduct
  },
  beforeMount() {
    this.getProducts();
  },
  data() {
    return {
      dialog: false,
      name: "catalog"
    };
  },
  methods: {
    ...mapActions(["getProducts"])
  }
};
</script>

<style scoped>
.products {
  display: flex;
  flex-wrap: wrap;
}
.products * {
  margin: 1vw !important;
  width: 20vw;
}
</style>

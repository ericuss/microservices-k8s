<template>
  <div class="catalog">
    <h3>{{name}}</h3>
    <div>
      <div v-for="product in products" v-bind:key="product.id">
        <v-card class="mx-auto" max-width="344">
            <v-img :src="product.src" class="white--text align-end" gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)" height="200px">
              <v-card-title v-text="product.title"></v-card-title>
            </v-img>
          <v-card-text>
            <p class="display-1 text--primary">{{ product.name }}</p>
          </v-card-text>
          <v-card-actions>
            <v-btn text color="deep-purple accent-4">Learn More</v-btn>
          </v-card-actions>
        </v-card>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import { mapGetters } from "vuex";

export default {
  name: "catalog",
  computed: {
    ...mapGetters(["products", "productsCount"])
  },
  data: () => ({
    dialog: false,
    name: "catalog"
  }),

  methods: {}
};
</script>

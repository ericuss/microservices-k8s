<template>
  <v-app id="inspire">
    <v-app-bar app color="indigo" dark>
      <!-- <v-app-bar-nav-icon @click.stop="drawer = !drawer" /> -->
      <v-toolbar-title>Lanre Shop</v-toolbar-title>
    </v-app-bar>

    <v-content>
      <v-container class="fill-height" fluid>
        <router-view></router-view>
      </v-container>
    </v-content>
    <v-footer color="indigo" app>
      <span class="white--text">&copy; 2019</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  props: {
    source: String
  },
  data: () => ({
    drawer: null
  }),
  computed: {
    menuItems() {
      return [
        { title: "Products", path: "/products", icon: "product" },
        ];
    }
  }
};
</script>

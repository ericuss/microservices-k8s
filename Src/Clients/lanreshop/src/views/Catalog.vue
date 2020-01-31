<template>
  <div class="catalog">
    <Catalog/>
  </div>
</template>

<script>
// @ is an alias to /src
import Catalog from '@/components/Catalog.vue'

export default {
  name: 'catalog',
  components: {
    Catalog
  }
}
</script>

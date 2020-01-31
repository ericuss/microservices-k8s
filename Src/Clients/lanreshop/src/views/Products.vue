<template>
  <div class="products">
    <Products/>
  </div>
</template>

<script>
// @ is an alias to /src
import Products from '@/components/Products.vue'

export default {
  name: 'products',
  components: {
    Products
  }
}
</script>

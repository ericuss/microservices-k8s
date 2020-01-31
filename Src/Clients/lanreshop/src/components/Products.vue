<template>
  <div class="products">
    <v-toolbar text color="white">
      <v-toolbar-title>Products</v-toolbar-title>
      <v-divider class="mx-2" inset vertical></v-divider>
      <v-spacer></v-spacer>
      <v-dialog v-model="dialog" max-width="500px">
        <template v-slot:activator="{ on }">
          <v-btn color="primary" dark class="mb-2" v-on="on">New Item</v-btn>
        </template>
        <v-card>
          <v-card-title>
            <span class="headline">{{ formTitle }}</span>
          </v-card-title>

          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.id" label="Id"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.name" label="Name"></v-text-field>
                </v-flex>
                <v-flex xs12 sm6 md4>
                  <v-text-field v-model="editedItem.stock" label="Stock"></v-text-field>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="blue darken-1" text @click="close">Cancel</v-btn>
            <v-btn color="blue darken-1" text @click="save">Save</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-toolbar>
      <!-- :pagination.sync="pagination" -->
      <!-- @page-count="pagination" -->
      <!-- :total-items="productsCount" -->
     <v-data-table
      :headers="headers"
      :items="products"
      :loading="loading"
      class="elevation-1">
      <!-- <template v-slot:items="props">
        <td class="text-xs-right">{{ props.item.name }}</td>
        <td class="text-xs-right">{{ props.item.name }}</td>
        <td class="text-xs-right">{{ props.item.stock }}</td>
        <td class="justify-center layout px-0">
          <v-icon small class="mr-2" @click="editItem(props.item)">edit</v-icon>
          <v-icon small @click="deleteItem(props.item)">delete</v-icon>
          asd
        </td> -->
        <!-- <td class="justify-center layout px-0">
          <v-icon small class="mr-2" @click="editItem(props.item)">edit</v-icon>
          <v-icon small @click="deleteItem(props.item)">delete</v-icon>
        </td> -->
      <!-- </template> -->
      <template v-slot:item.actions="{ item }">
          <v-icon small class="mr-2" @click="editItem(item)">edit</v-icon>
          <v-icon small @click="deleteItem(item)">delete</v-icon>
      </template>
      <!-- <template v-slot:no-data>
        <v-btn color="primary" @click="initialize">Reset</v-btn>
      </template> -->
    </v-data-table>
  </div>
  
</template>

<script>
// @ is an alias to /src
import { mapGetters, mapActions } from 'vuex'

export default {
  name: 'products',
  computed: {
    ...mapGetters([
      'products',
      'productsCount'
    ]),
    formTitle () {
      return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
    }
  },
  data: () => ({
    dialog: false,
    name: 'Products',
    headers: [
      {
        text: 'Id',
        align: 'left',
        sortable: false,
        value: 'id'
      },
      { text: 'Name', value: 'name' },
      { text: 'Stock', value: 'stock', sortable: false },
      { text: 'Actions', value: 'actions', sortable: false }
    ],
    pagination: {},
    loading: true,
    editedIndex: -1,
    editedItem: {
      id: 0,
      name: ''
    },
  }),

  watch: {
    dialog (val) {
      val || this.close()
    },
    pagination: {
      handler () {
        // this.clients
        //   .then(data => {
        //     this.desserts = data.items
        //     this.totalDesserts = data.total
        //   })
      },
      deep: true
    }
  },

  methods: {
    ...mapActions([
      'setNewProduct',
      'updateProduct',
      'removeProduct'
    ]),
    editItem (item) {
      this.editedIndex = this.products.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem (item) {
      confirm(`Are you sure you want to delete the product ${item.name} item?`) && this.removeProduct(item.id)
    },

    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },

    async save () {
      if (this.editedIndex > -1) {
        // Object.assign(this.desserts[this.editedIndex], this.editedItem)
        await this.updateProduct(this.editedItem)
      } else {
        await this.setNewProduct(this.editedItem)
      }
      this.close()
    }
  }
}
</script>

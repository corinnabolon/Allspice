<template>
  <div>
    <form @submit.prevent="createRecipe">
      <div>
        <label for="title" class="form-label">Title</label>
        <input v-model="editable.title" type="text" class="form-control" id="title" placeholder="Title..." required
          maxLength="255">
      </div>
      <div class="mb-3">
        <label for="category" class="form-label">Category</label>
        <select v-model="editable.category" class="form-select" aria-label="Default select example" required>
          <option v-for="category in categories" :key="categories" :value="category">{{ category }}</option>
        </select>
      </div>
      <div>
        <label for="img" class="form-label">Image URL</label>
        <input v-model="editable.img" type="text" class="form-control" id="title" placeholder="Image URL..." required
          maxLength="1000">
      </div>
      <button type="button" class="btn btn-danger" data-bs-dismiss="modal" aria-label="Close">Close</button>
      <button type="submit" class="btn btn-success">Submit</button>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})

    return {
      editable,
      categories: [
        "Cheese",
        "Soup",
        "Mexican",
        "Italian",
        "Specialty Coffee"
      ],

      async createRecipe() {
        try {
          let recipeData = editable.value
          await recipesService.createRecipe(recipeData)
          Pop.success("Recipe created! Now add the instructions and ingredients.")
          editable.value = {}
          Modal.getOrCreateInstance('#createRecipeModal').hide()
          Modal.getOrCreateInstance('#recipeCardModal').show()
        } catch (error) {
          Pop.error(error)
        }
      }



    }
  }
};
</script>


<style lang="scss" scoped></style>
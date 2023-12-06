<template>
  <div>
    <form @submit.prevent="createRecipe">
      <div class="mb-3">
        <label for="title" class="form-label" aria-label="Recipe Title">
          <p class="serif-font fs-5 mb-0">Title</p>
        </label>
        <input v-model="editable.title" type="text" class="form-control" id="title" placeholder="Title..." required
          maxLength="255">
      </div>
      <div class="mb-3">
        <label for="category" class="form-label">
          <p class="serif-font fs-5 mb-0">Category</p>
        </label>
        <select v-model="editable.category" class="form-select" aria-label="Recipe Category" required>
          <option v-for="category in categories" :key="categories" :value="category">{{ category }}</option>
        </select>
      </div>
      <div>
        <label for="img" class="form-label" aria-label="Rcipe Image URL">
          <p class="serif-font fs-5 mb-0">Image URL</p>
        </label>
        <input v-model="editable.img" type="text" class="form-control" id="title" placeholder="Image URL..." required
          maxLength="1000">
      </div>
      <div class="d-flex justify-content-end mt-3">
        <button type="button" class="btn btn-theme-orange me-3" data-bs-dismiss="modal" aria-label="Close">Close</button>
        <button type="submit" class="btn btn-theme-green">Submit</button>
      </div>
    </form>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, onUnmounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js";
import { Modal } from "bootstrap";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    const editable = ref({})

    onMounted(() => {
      let deleteIngredientsModalElem = document.getElementById('createRecipeModal')
      deleteIngredientsModalElem.addEventListener('hidden.bs.modal', function (event) {
        editable.value = {}
      })
    })

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
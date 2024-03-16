<template>
  <div v-for="ingredient in activeRecipeIngredients" :key="ingredient.id">
    <p class="fs-3"><i @click="removeIngredient(ingredient.id)" role="button" title="Delete this ingredient"
        class="mdi mdi-delete"></i>{{ ingredient.quantity }} {{
    ingredient.name
  }}</p>
  </div>
  <div class="text-end">
    <button class="btn btn-theme-orange" @click="goBack()" aria-label="Close">Go Back</button>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { recipesService } from '../services/RecipesService.js';
import Pop from "../utils/Pop.js";
import { ingredientsService } from "../services/IngredientsService.js";
import { Modal } from "bootstrap";

export default {
  setup() {


    return {
      activeRecipeIngredients: computed(() => AppState.activeRecipeIngredients),

      async removeIngredient(ingredientId) {
        try {
          let wantsToDelete = await Pop.confirm("Are you sure you want to delete this ingredient?")
          if (!wantsToDelete) {
            return
          }
          await ingredientsService.removeIngredient(ingredientId)
          Pop.success("Ingredient deleted from this recipe.")
          Modal.getOrCreateInstance('#deleteIngredientsModal').hide()
          Modal.getOrCreateInstance('#recipeCardModal').show()
        } catch (error) {
          Pop.error(error)
        }
      },

      goBack() {
        Modal.getOrCreateInstance('#deleteIngredientsModal').hide()
        Modal.getOrCreateInstance('#recipeCardModal').show()
      }

    }
  }
};
</script>


<style lang="scss" scoped></style>
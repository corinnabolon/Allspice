<template>
  <div @click="setActiveRecipe(recipeProp.id)" role="button">
    <img :src="recipeProp.img" alt="recipe image" class="recipe-img">
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { recipesService } from '../services/RecipesService.js';


export default {
  props: { recipeProp: { type: Recipe, required: true } },

  setup() {
    return {
      setActiveRecipe(recipeId) {
        logger.log("Recipe ID", recipeId)
        recipesService.setActiveRecipe(recipeId);
        logger.log("AppState.activeRecipe", AppState.activeRecipe)
      }


    }
  }
};
</script>


<style lang="scss" scoped>
.recipe-img {
  height: 30vh;
  width: 20vw;
  object-fit: cover;
}
</style>
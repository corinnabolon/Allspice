<template>
  <div @click="setActiveRecipe(recipeProp.id)">
    <div class="recipeCard-coverImg" @click="openModal" role="button" title="See recipe details">
      <section class="row d-flex justify-content-between px-2">
        <div class="col-6 fs-5">
          <p class="recipeCard-words rounded-pill text-center mt-2">
            {{ recipeProp.category }}
          </p>
        </div>
        <div class="col-2">
          <p class="recipeCard-words rounded text-center fs-3">
            <i v-if="isFavRecipe" @click.stop="deleteFavorite(recipeProp.id)" role="button"
              class="mdi mdi-heart text-danger" title="Unfavorite this recipe"></i>
            <i v-else @click.stop="createFavorite(recipeProp.id)" role="button" class="mdi mdi-heart-outline"
              title="Favorite this recipe"></i>
          </p>
        </div>
      </section>
      <section class="row">
        <div class="col-10">
          <span class="d-flex align-items-center justify-content-center title-box position-end recipeCard-words rounded">
            <p class="fs-5 m-1">{{ recipeProp.title }}</p>
          </span>
        </div>
      </section>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { recipesService } from '../services/RecipesService.js';
import Pop from "../utils/Pop.js";
import { favoritesService } from "../services/FavoritesService.js";
import { Modal } from "bootstrap";


export default {
  props: { recipeProp: { type: Recipe, required: true } },

  setup(props) {
    return {
      recipeCoverImg: computed(() => `url(${props.recipeProp.img})`),
      isFavRecipe: computed(() => AppState.myFavoriteRecipes.find((recipe) => recipe.id == props.recipeProp.id)),

      setActiveRecipe(recipeId) {
        logger.log("Recipe ID", recipeId)
        recipesService.setActiveRecipe(recipeId);
      },

      async createFavorite(recipeId) {
        try {
          await favoritesService.createFavorite(recipeId)
          Pop.success("You have favorited this recipe.")
        } catch (error) {
          Pop.error(error)
        }
      },

      async deleteFavorite(recipeId) {
        try {
          await favoritesService.deleteFavorite(recipeId)
          Pop.success("You have unfavorited this recipe.")
        } catch (error) {
          Pop.error(error)
        }
      },

      openModal() {
        Modal.getOrCreateInstance('#recipeCardModal').show();
      }


    }
  }
};
</script>


<style lang="scss" scoped>
.recipeCard-coverImg {
  position: relative;
  background-image: v-bind(recipeCoverImg);
  background-size: cover;
  background-position: center;
  height: 22rem;
  width: 20rem;
  margin: 2rem;
}

.recipeCard-words {
  color: white;
  background-color: rgba(82, 69, 69, 0.12);
  backdrop-filter: blur(13px);
  font-weight: bold;
}

.position-end {
  position: absolute;
  left: 5%;
  top: 79%;
}

.title-box {
  height: 4rem;
  width: 18rem;
}
</style>
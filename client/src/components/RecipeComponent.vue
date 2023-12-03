<template>
  <div @click="setActiveRecipe(recipeProp.id)" role="button">
    <div class="recipeCard-coverImg">
      <section class="row d-flex justify-content-between">
        <!-- <img :src="recipeProp.img" alt="recipe image" class="recipe-img"> -->
        <div class="col-6 fs-5">
          <p class="recipeCard-words rounded-pill text-center mt-2">
            {{ recipeProp.category }}
          </p>
        </div>
        <div class="col-2">
          <p class="recipeCard-words rounded text-center fs-3">
            <i class="mdi mdi-heart"></i>
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


export default {
  props: { recipeProp: { type: Recipe, required: true } },

  setup(props) {
    return {
      recipeCoverImg: computed(() => `url(${props.recipeProp.img})`),
      myFavoriteRecipes: computed(() => AppState.myFavoriteRecipes),

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
// .recipe-img {
//   height: 30vh;
//   width: 20vw;
//   object-fit: cover;
// }

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
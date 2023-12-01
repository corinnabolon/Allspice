<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-6 d-flex justify-content-around">
        <p @click="goHome()" role="button">Home</p>
        <p @click="flipWantsMyRecipes()" role="button">My Recipes</p>
        <p @click="flipWantsFavorites()" role="button">Favorites</p>
      </div>
    </section>
    <section v-if="recipes" class="row justify-content-center">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-3 m-3">
        <RecipeComponent :recipeProp="recipe" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watch, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService.js';
import RecipeComponent from '../components/RecipeComponent.vue';
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    let account = computed(() => AppState.account)
    let wantsFavorites = ref(false)
    let wantsMyRecipes = ref(false)

    onMounted(() => {
      getRecipes();
    })


    watch(account, () => {
      getFavoritedRecipes();
    });


    async function getRecipes() {
      try {
        await recipesService.getRecipes();
        // logger.log("Test of filter", AppState.recipes.filter(recipe => recipe.creatorId != account.id))
      } catch (error) {
        Pop.error(error);
      }
    }

    async function getFavoritedRecipes() {
      try {
        await accountService.getMyFavoritedRecipes();
      } catch (error) {
        Pop.error(error);
      }
    }


    return {
      wantsFavorites,
      wantsMyRecipes,
      account,

      recipes: computed(() => {
        if (wantsFavorites.value) {
          return AppState.myFavoriteRecipes
        } else if (wantsMyRecipes.value) {
          return AppState.recipes.filter(recipe => recipe.creatorId == AppState.account.id)
        }
        return AppState.recipes
      }),

      flipWantsFavorites() {
        wantsFavorites.value = !wantsFavorites.value;
        wantsMyRecipes.value = false;
      },

      flipWantsMyRecipes() {
        wantsMyRecipes.value = !wantsMyRecipes.value;
        wantsFavorites.value = false;
      },

      goHome() {
        wantsMyRecipes.value = false;
        wantsFavorites.value = false;
      }

    }
  },
  components: { RecipeComponent }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>

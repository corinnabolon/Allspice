<template>
  <div class="container-fluid">
    <!-- <section class="row justify-content-center">
      <div class="col-6 d-flex justify-content-around">
        <p @click="goHome()" role="button">Home</p>
        <p @click="flipWantsMyRecipes()" role="button">My Recipes</p>
        <p @click="flipWantsFavorites()" role="button">Favorites</p>
      </div>
    </section> -->
    <section v-if="recipes" class="row justify-content-evenly large-margin-top">
      <div v-for="recipe in recipes" :key="recipe.id" class="col-3 m-2 recipe-component">
        <RecipeComponent :recipeProp="recipe" />
      </div>
    </section>
  </div>

  <ModalComponent :modalId="'recipeCardModal'" :modalSize="'modal-xl'">
    <template #modalTitle>
      <p v-if="activeRecipe" class="serif-font fs-2">
        {{ activeRecipe.title }}</p>
    </template>
    <template #modalBody>
      <RecipeDetailsComponent />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'createRecipeModal'" :modalSize="'modal-md'">
    <template #modalTitle>
      <p class="serif-font fs-3">New Recipe</p>
    </template>
    <template #modalBody>
      <NewRecipeForm />
    </template>
  </ModalComponent>

  <ModalComponent :modalId="'deleteIngredientsModal'" :modalSize="'modal-md'">
    <template #modalTitle>
      <p class="serif-font fs-5">Which ingredients would you like to delete?</p>
    </template>
    <template #modalBody>
      <DeleteIngredientsComponent />
    </template>
  </ModalComponent>
</template>


<script>
import { computed, onMounted, watch, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService.js';
import RecipeComponent from '../components/RecipeComponent.vue';
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";
import ModalComponent from '../components/ModalComponent.vue';
import NewRecipeForm from '../components/NewRecipeForm.vue';
import RecipeDetailsComponent from '../components/RecipeDetailsComponent.vue';
import DeleteIngredientsComponent from '../components/DeleteIngredientsComponent.vue';

export default {
  setup() {
    let account = computed(() => AppState.account)
    // let wantsFavorites = ref(false)
    // let wantsMyRecipes = ref(false)
    let wantsFavorites = ref(AppState.wantsFavorites)
    let wantsMyRecipes = ref(AppState.wantsMyRecipes)

    onMounted(() => {
      getRecipes();
    })


    watch(account, () => {
      getFavoritedRecipes();
    });


    async function getRecipes() {
      try {
        await recipesService.getRecipes();
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

      activeRecipe: computed(() => AppState.activeRecipe),
      activeRecipeIngredients: computed(() => AppState.activeRecipeIngredients),

      // recipes: computed(() => {
      //   if (wantsFavorites.value) {
      //     return AppState.myFavoriteRecipes
      //   } else if (wantsMyRecipes.value) {
      //     return AppState.recipes.filter(recipe => recipe.creatorId == AppState.account.id)
      //   }
      //   return AppState.recipes
      // }),

      recipes: computed(() => {
        if (AppState.wantsFavorites) {
          return AppState.myFavoriteRecipes
        } else if (AppState.wantsMyRecipes) {
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
      },

    }
  },
  components: { RecipeComponent, ModalComponent, NewRecipeForm, RecipeDetailsComponent, DeleteIngredientsComponent }
}
</script>

<style scoped lang="scss">
.recipe-component:hover {
  transform: scale(1.03);
  transition: all 0.15s linear;
}

.large-margin-top {
  margin-top: 6rem;
}

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

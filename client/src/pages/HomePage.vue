<template>
  <div class="container-fluid">
    <section class="row navbar-coverImg">
      <div class="col-12">
        <section class="row">
          <div class="col-md-2"></div>
          <div class="col-6 col-md-4 serif-font font-beige text-shadow pb-4 mt-5 mb-md-2">
            <p class="fs-1">All-Spice</p>
            <p class="fs-2">Cherish your family</p>
            <p class="fs-2">And their cooking</p>
          </div>
          <div class="col-2 col-md-4"></div>
          <div class="col-4 col-md-2 d-flex align-items-end justify-content-end">
            <div v-if="account.id">
              <button data-bs-toggle="modal" data-bs-target="#createRecipeModal"
                class="btn btn-success btn-theme-green mb-5 mb-md-0">Create
                Recipe</button>
            </div>
          </div>
        </section>
        <section class="row position-relative justify-content-center align-items-center">
          <div
            class="col-6 d-flex justify-content-between px-md-5 align-items-center link-box serif-font fs-3 text-theme-green">
            <p @click="goHome()" role="button">Home</p>
            <p @click="flipWantsMyRecipes()" role="button">My Recipes</p>
            <p @click="flipWantsFavorites()" role="button">Favorites</p>
          </div>
        </section>

        <section v-if="recipes" class="row justify-content-evenly large-margin-top">
          <div v-for="recipe in recipes" :key="recipe.id" class="col-12 col-md-3 m-md-2 recipe-component">
            <RecipeComponent :recipeProp="recipe" />
          </div>
        </section>


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
    let wantsFavorites = ref(false)
    let wantsMyRecipes = ref(false)
    // let wantsFavorites = ref(AppState.wantsFavorites)
    // let wantsMyRecipes = ref(AppState.wantsMyRecipes)

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

      navbarCoverImg: computed(() => `url(https://images.unsplash.com/photo-1483137140003-ae073b395549?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)`),

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
      },

    }
  },
  components: { RecipeComponent, ModalComponent, NewRecipeForm, RecipeDetailsComponent, DeleteIngredientsComponent }
}
</script>

<style scoped lang="scss">
.navbar-coverImg {
  background-image: v-bind(navbarCoverImg);
  background-size: cover;
  background-position: center;
  height: 20rem;
  background-attachment: fixed;
}

.link-box {
  position: absolute;
  height: 20vh;
  width: 50vw;
  background-color: whitesmoke;
  border-radius: 1rem;
  box-shadow: 5px 5px 5px gray;
  top: -1rem;
}

.text-shadow {
  text-shadow: 1px 1px 2px var(--theme-green), 1px 1px 2px var(--theme-green), 1px 1px 2px var(--theme-green);
}

.font-beige {
  color: beige;
}

.recipe-component:hover {
  transform: scale(1.03);
  transition: all 0.15s linear;
}

.large-margin-top {
  margin-top: 6rem;
}

.position-login {
  position: absolute;
  left: 70%;
  top: 5%;
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

@media screen and (max-width: 768px) {
  .link-box {
    height: 15vh;
    width: 90vw;
  }
}
</style>

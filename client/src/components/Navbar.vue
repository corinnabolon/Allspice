<template>
  <!-- //fixed-top to the navbar -->
  <nav class="navbar-expand-sm position-relative">
    <div class="d-flex align-items-center justify-content-end"
      :class="route.name != 'Account' ? 'position-login' : 'position-account-login'">
      <div v-if="route.name != 'Account'" class="me-3 me-md-5">
        <form @submit.prevent="searchRecipeByCategory" class="d-flex align-items-center position-relative">
          <input v-model="editable" title="Search recipe by category" type="text" class="form-control input-position"
            id="searchbar" aria-describedby="searchBar" placeholder="Search by category..." minlength="1" maxlength="50">
          <button class="fs-3" title="Search recipe by category"><i class="mdi mdi-magnify search-position" role="button"
              type="submit"></i></button>
        </form>
      </div>
      <div v-else>
        <router-link :to="{ name: 'Home' }">
          <p class="fs-3 serif-font text-theme-green me-5 mx-4">Home</p>
        </router-link>
      </div>
      <!-- <button class="btn text-dark" @click="toggleTheme"><i class="mdi"
          :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button> -->
      <Login />
    </div>
    <!-- <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img alt="logo" src="../assets/img/cw-logo.png" height="45" />
      </div>
    </router-link> -->
    <button class="navbar-toggler position-login" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
    </div>
  </nav>
</template>

<script>
import { AppState } from '../AppState';
import { onMounted, ref, computed } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { recipesService } from '../services/RecipesService.js';
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";


export default {
  setup() {
    let editable = ref("")
    let wantsFavorites = ref(AppState.wantsFavorites)
    let wantsMyRecipes = ref(AppState.wantsMyRecipes)
    const route = useRoute();

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
      editable,
      route,
      wantsFavorites,
      wantsMyRecipes,
      account: computed(() => AppState.account),
      navbarCoverImg: computed(() => `url(https://images.unsplash.com/photo-1483137140003-ae073b395549?q=80&w=1470&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D)`),
      theme,
      toggleTheme() {
        theme.value = theme.value == 'light' ? 'dark' : 'light'
        document.documentElement.setAttribute('data-bs-theme', theme.value)
        saveState('theme', theme.value)
      },

      flipWantsFavorites() {
        AppState.wantsFavorites = !AppState.wantsFavorites;
        AppState.wantsMyRecipes = false;
      },

      flipWantsMyRecipes() {
        AppState.wantsMyRecipes = !AppState.wantsMyRecipes;
        AppState.wantsFavorites = false;
      },

      goHome() {
        AppState.wantsMyRecipes = false;
        AppState.wantsFavorites = false;
      },

      async searchRecipeByCategory() {
        try {
          let query = editable.value
          await recipesService.searchRecipesByCategory(query)
          editable.value = ""
        } catch (error) {
          Pop.error(error)
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>
button {
  border: none;
  background-color: none;
}

.search-position {
  position: absolute;
  top: 0%;
  left: 85%;
}

.position-login {
  position: absolute;
  left: 70%;
  top: 5%;
}

.position-account-login {
  position: absolute;
  left: 85%;
  top: 5%;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (max-width: 768px) {

  .input-position {
    position: absolute;
    left: -190px;
    width: 200px;
    z-index: 1;
  }

  .search-position {
    top: -13px;
    left: -12px;
    z-index: 1;
  }

  .position-login {
    left: 70%;
    z-index: 2;
    top: 2%;
  }

  .position-account-login {
    left: 70%;
  }
}
</style>../Store.js
<template>
  <nav class="navbar navbar-expand-sm navbar-coverImg position-relative">
    <div class="position-login d-flex align-items-center justify-content-end">
      <div>
        <form class="d-flex align-items-center position-relative">
          <input type="test" class="form-control" id="searchbar" aria-describedby="searchBar" placeholder="Search..."
            minlength="1" maxlength="50">
          <p class="fs-3"><i class="mdi mdi-magnify search-position" role="button" type="submit"></i></p>
        </form>
      </div>
      <button class="btn text-dark" @click="toggleTheme"><i class="mdi"
          :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
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
      <div class="container">
        <section class="row">
          <div class="col-2"></div>
          <div class="col-4 serif-font font-beige text-shadow pb-5">
            <p class="fs-1">All-Spice</p>
            <p class="fs-2">Cherish your family</p>
            <p class="fs-2">And their cooking</p>
          </div>
          <div class="col-4"></div>
          <div class="col-2 d-flex align-items-end justify-content-end">
            <div v-if="account.id">
              <button data-bs-toggle="modal" data-bs-target="#createRecipeModal"
                class="btn btn-success btn-theme-green">Create
                Recipe</button>
            </div>
          </div>
        </section>
        <section class="row position-relative justify-content-center align-items-center">
          <div class="col-6 d-flex justify-content-around align-items-center link-box serif-font fs-3 text-theme-green">
            <p @click="goHome()" role="button">Home</p>
            <p @click="flipWantsMyRecipes()" role="button">My Recipes</p>
            <p @click="flipWantsFavorites()" role="button">Favorites</p>
          </div>
        </section>
      </div>
      <!-- <ul class="navbar-nav me-auto">
        <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li>
      </ul> -->
      <!-- LOGIN COMPONENT HERE -->
      <!-- <div>
        <button class="btn text-light bg-dark" @click="toggleTheme"><i class="mdi"
            :class="theme == 'light' ? 'mdi-weather-sunny' : 'mdi-weather-night'"></i></button>
      </div>
      <Login /> -->
    </div>
  </nav>
</template>

<script>
import { AppState } from '../AppState';
import { onMounted, ref, computed } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { recipesService } from '../services/RecipesService.js';

export default {
  setup() {
    let wantsFavorites = ref(AppState.wantsFavorites)
    let wantsMyRecipes = ref(AppState.wantsMyRecipes)

    const theme = ref(loadState('theme') || 'light')

    onMounted(() => {
      document.documentElement.setAttribute('data-bs-theme', theme.value)
    })

    return {
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
    }
  },
  components: { Login }
}
</script>

<style scoped>
.link-box {
  position: absolute;
  height: 20vh;
  width: 50vw;
  background-color: whitesmoke;
  border-radius: 1rem;
  box-shadow: 5px 5px 5px gray;
  top: -1rem;
}

.search-position {
  position: absolute;
  top: 0%;
  left: 89%;
}

.position-login {
  position: absolute;
  left: 70%;
  top: 5%;
}

.text-shadow {
  text-shadow: 1px 1px 2px var(--theme-green), 1px 1px 2px var(--theme-green), 1px 1px 2px var(--theme-green);
}

.font-beige {
  color: beige;
}

.navbar-coverImg {
  background-image: v-bind(navbarCoverImg);
  background-size: cover;
  background-position: center;
  height: 20rem;
  background-attachment: fixed;
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

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>

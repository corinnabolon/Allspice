<template>
  <div v-if="recipes">
    {{ recipes }}
  </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { recipesService } from '../services/RecipesService.js';

export default {
  setup() {

    onMounted(() => {
      getRecipes();
    })


    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        Pop.error(error)
      }
    }


    return {
      recipes: computed(() => AppState.recipes),

    }
  }
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

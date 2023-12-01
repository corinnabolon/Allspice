<template>
  <section v-if="activeRecipe" class="row reci-card">
    <div class="col-4">
      <img :src="activeRecipe.img" class="reci-img">
    </div>
    <div class="col-8">
      <section class="row">
        <div class="col-12">
          <div class="d-flex justify-content-around">
            <p>{{ activeRecipe.title }}</p>
            <p>{{ activeRecipe.category }}</p>
          </div>
        </div>
        <div class="col-5">
          <p>Recipe Instructions</p>
          <p>{{ activeRecipe.instructions }}</p>
        </div>
        <div v-if="activeRecipeIngredients" class="col-5">
          <p>Ingredients</p>
          <div v-for="ingredient in activeRecipeIngredients" :key="ingredient.id">
            <p>{{ ingredient.quantity }} {{ ingredient.name }}</p>
          </div>
        </div>
      </section>
      <section class="row">
        <div class="col-5">
          <button class="btn btn-success">Add Instructions</button>
        </div>
      </section>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  setup() {
    return {
      activeRecipe: computed(() => AppState.activeRecipe),
    }
  }
};
</script>


<style lang="scss" scoped>
.reci-img {
  height: 100%;
  width: 100%;
  object-fit: cover;
}

.reci-card {
  height: 100%;
}
</style>
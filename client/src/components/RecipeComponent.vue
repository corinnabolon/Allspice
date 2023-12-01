<template>
  <div>
    <img :src="recipeProp.img" alt="recipe image" class="recipe-img">
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe.js";


export default {
  props: { recipeProp: { type: Recipe, required: true } },

  setup() {
    return {}
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
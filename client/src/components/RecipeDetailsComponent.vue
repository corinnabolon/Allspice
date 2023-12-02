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
          <div v-if="!addingInstructions">
            <p>{{ activeRecipe.instructions }}</p>
          </div>
          <div v-else>
            <form>
              <label for="recipeInstructions" class="form-label">Fill out instructions below:</label>
              <textarea v-model="editable" class="form-control" id="recipeInstructions"
                aria-describedby="recipeInstructions" maxLength="1000" />
            </form>
          </div>
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
          <div>
            <button v-if="!addingInstructions && !editable" @click="flipInstructionTextarea" class="btn btn-success">Add
              Instructions</button>
            <button v-if="!addingInstructions && editable" @click="flipInstructionTextarea" class="btn btn-success">Edit
              Instructions</button>
          </div>
          <div v-if="addingInstructions" class="d-flex">
            <button @click="flipInstructionTextarea" class="btn btn-danger">Cancel Changes</button>
            <button class="btn btn-success">Submit Changes</button>
          </div>
        </div>
      </section>
    </div>
  </section>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref, watchEffect } from 'vue';
import { logger } from "../utils/Logger.js";


export default {
  setup() {
    let addingInstructions = ref(false);
    let editable = ref("")

    watchEffect(() => {
      if (AppState.activeRecipe) {
        if (AppState.activeRecipe.instructions) {
          editable.value = (AppState.activeRecipe.instructions)
          logger.log('WATCH')
        } else {
          editable.value = ""
        }
      }
      else {
        editable.value = ""
      }
    })

    return {
      editable,
      addingInstructions,
      activeRecipe: computed(() => AppState.activeRecipe),
      activeRecipeIngredients: computed(() => AppState.activeRecipeIngredients),

      flipInstructionTextarea() {
        addingInstructions.value = !addingInstructions.value;
      }
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
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
              <textarea v-model="editableInstructions" class="form-control" id="recipeInstructions"
                aria-describedby="recipeInstructions" maxLength="1000" />
            </form>
          </div>
        </div>
        <div class="col-5">
          <p v-if="activeRecipeIngredients.length">Ingredients</p>
          <div v-for="ingredient in activeRecipeIngredients" :key="ingredient.id">
            <p>{{ ingredient.quantity }} {{ ingredient.name }}</p>
          </div>
          <div>
            <button v-if="!addingIngredients" @click="flipIngredientTextarea" class="btn btn-success">Add an
              Ingredient</button>
            <div v-else class="d-flex">
              <div>
                <label for="name" class="form-label">Name of Ingredient</label>
                <input v-model="editableIngredient.name" type="text" class="form-control" id="name" placeholder="Cinnamon"
                  required maxLength="255">
              </div>
              <div>
                <label for="quantity" class="form-label">Quantity of Ingredient</label>
                <input v-model="editableIngredient.quantity" type="text" class="form-control" id="quantity"
                  placeholder="1 tsp" required maxLength="255">
              </div>
            </div>
          </div>
          <div v-if="addingIngredients" class="d-flex">
            <button @click="flipIngredientTextarea" class="btn btn-danger">Finished</button>
            <button @click="addIngredient" class="btn btn-success">Add Ingredient</button>
          </div>
        </div>
      </section>
      <section class="row">
        <div class="col-5">
          <div>
            <button v-if="!addingInstructions && !editableInstructions" @click="flipInstructionTextarea"
              class="btn btn-success">Add
              Instructions</button>
            <button v-if="!addingInstructions && editableInstructions" @click="flipInstructionTextarea"
              class="btn btn-success">Edit
              Instructions</button>
          </div>
          <div v-if="addingInstructions" class="d-flex">
            <button @click="reloadEditableInstructions" class="btn btn-danger">Cancel Changes</button>
            <button @click="addInstructions" class="btn btn-success">Submit Changes</button>
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
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js"
import { ingredientsService } from "../services/IngredientsService.js"


export default {
  setup() {
    let addingInstructions = ref(false);
    let addingIngredients = ref(false);
    let editableInstructions = ref("")
    let editableIngredient = ref({})

    watchEffect(() => {
      if (AppState.activeRecipe) {
        if (AppState.activeRecipe.instructions) {
          editableInstructions.value = (AppState.activeRecipe.instructions)
          logger.log('WATCH')
        } else {
          editableInstructions.value = ""
        }
      }
      else {
        editableInstructions.value = ""
      }
    })

    return {
      editableInstructions,
      editableIngredient,
      addingInstructions,
      addingIngredients,
      activeRecipe: computed(() => AppState.activeRecipe),
      activeRecipeIngredients: computed(() => AppState.activeRecipeIngredients),

      flipInstructionTextarea() {
        addingInstructions.value = !addingInstructions.value;
      },

      flipIngredientTextarea() {
        addingIngredients.value = !addingIngredients.value;
      },

      reloadEditableInstructions() {
        this.flipInstructionTextarea()
        editableInstructions.value = (AppState.activeRecipe.instructions)
      },

      async addInstructions() {
        try {
          let instructions = editableInstructions.value
          await recipesService.addInstructions(instructions)
          addingInstructions.value = false
          Pop.success("Instructions modified!")
        } catch (error) {
          Pop.error(error)
        }
      },

      async addIngredient() {
        try {
          let ingredient = editableIngredient.value
          logger.log(ingredient)
          await ingredientsService.addIngredient(ingredient)
          editableIngredient.value = {}
          Pop.success("Ingredient added!")
        } catch (error) {
          Pop.error(error)
        }
      },


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
<template>
  <section v-if="activeRecipe" class="row reci-card">
    <div class="col-12 col-md-4 position-relative">
      <img :src="activeRecipe.img" class="reci-img">
      <div>
        <div v-if="account.id == activeRecipe.creatorId">
          <button @click="removeRecipe(activeRecipe.id)" class="btn btn-theme-orange delete-recipe-button">Delete
            Recipe</button>
        </div>
        <p v-if="account.id" class="recipeCard-words rounded-bottom text-center fs-3 px-1">
          <i v-if="isFavRecipe" @click.stop="removeFavorite(activeRecipe.id)" role="button"
            class="mdi mdi-heart text-danger" title="Unfavorite this recipe"></i>
          <i v-else @click.stop="createFavorite(activeRecipe.id)" role="button" class="mdi mdi-heart-outline"
            title="Favorite this recipe"></i>
        </p>
      </div>
    </div>
    <div class="col-12 col-md-8 d-flex flex-column justify-content-between">
      <section class="row">
        <div class="col-12 mt-3 mt-md-0">
          <div class="d-flex justify-content-end serif-font fs-4">
            <p class="bg-theme-green text-theme-pink px-3 rounded-pill">{{ activeRecipe.category }}</p>
          </div>
        </div>
        <div class="col-12 col-md-5 px-3">
          <p class="serif-font fs-4">Recipe Instructions</p>
          <div v-if="!addingInstructions">
            <p class="serif-font">{{ activeRecipe.instructions }}</p>
            <div class="col-12 col-md-5 mt-2 mt-md-0 px-md-3 invisible-on-desktop align-items-end">
              <div v-if="account.id == activeRecipe.creatorId">
                <button v-if="!addingInstructions && !editableInstructions" @click="flipInstructionTextarea"
                  class="btn btn-theme-green fs-5 align-self-bottom">Add
                  Instructions</button>
                <button v-if="!addingInstructions && editableInstructions" @click="flipInstructionTextarea"
                  class="btn btn-theme-green fs-5">Edit
                  Instructions</button>
              </div>
            </div>
          </div>
          <div v-else>
            <form @submit.prevent="addInstructions()" id="add-instructions">
              <label for="recipeInstructions" class="form-label serif-font fs-5">Fill out instructions below:</label>
              <textarea v-model="editableInstructions" class="form-control" id="recipeInstructions"
                aria-describedby="recipeInstructions" minlength="3" maxlength="1000" />
            </form>
            <div class="invisible-on-desktop justify-content-center mt-3 mt-md-0">
              <button @click="reloadEditableInstructions" class="btn btn-theme-orange me-3">Cancel
                Changes</button>
              <button class="btn btn-theme-green p-2" type="submit" form="add-instructions">Submit Changes</button>
            </div>
          </div>
        </div>
        <div class="col-12 col-md-5 px-3">
          <p class="serif-font fs-4 mt-4 mt-md-0">Ingredients</p>
          <div v-if="!editingIngredients && !ingredientsAreHidden">
            <div v-for="ingredient in activeRecipeIngredients" :key="ingredient.id">
              <p class="serif-font fs-5">・{{ ingredient.quantity }} {{ ingredient.name }}</p>
            </div>
          </div>
        </div>
      </section>
      <section class="row">
        <div class="col-12 col-md-5 px-3 invisible-on-mobile align-items-end">
          <div v-if="account.id == activeRecipe.creatorId">
            <button v-if="!addingInstructions && !editableInstructions" @click="flipInstructionTextarea"
              class="btn btn-theme-green fs-5 align-self-bottom">Add
              Instructions</button>
            <button v-if="!addingInstructions && editableInstructions" @click="flipInstructionTextarea"
              class="btn btn-theme-green fs-5">Edit
              Instructions</button>
          </div>
          <div v-if="addingInstructions" class="invisible-on-mobile justify-content-center">
            <button @click="reloadEditableInstructions" class="btn btn-theme-orange p-2 me-3">Cancel
              Changes</button>
            <button class="btn btn-theme-green" type="submit" form="add-instructions">Submit Changes</button>
          </div>
        </div>
        <div class="col-12 col-md-7">
          <div v-if="account.id == activeRecipe.creatorId">
            <div v-if="!addingIngredients && !editingIngredients">
              <div>
                <button @click="flipIngredientTextarea" class="btn btn-theme-green fs-5 mb-md-1">Add Ingredients</button>
              </div>
              <div class="invisible-on-mobile">
                <button v-if="activeRecipeIngredients.length" @click="flipEditIngredientsForm"
                  class="btn btn-theme-green fs-5">Edit
                  Ingredients</button>
                <button v-if="activeRecipeIngredients.length" class="btn btn-theme-orange ms-2" data-bs-toggle="modal"
                  data-bs-target="#deleteIngredientsModal">Select
                  Ingredients to Delete</button>
              </div>
              <div class="invisible-on-desktop">
                <div>
                  <button v-if="activeRecipeIngredients.length" @click="flipEditIngredientsForm"
                    class="btn btn-theme-green fs-5">Edit
                    Ingredients</button>
                  <button v-if="activeRecipeIngredients.length" class="btn btn-theme-orange ms-md-2 mt-2 mt-md-0"
                    data-bs-toggle="modal" data-bs-target="#deleteIngredientsModal">Select
                    Ingredients to Delete</button>
                </div>
              </div>
            </div>
            <div v-else-if="addingIngredients && !editingIngredients" class="d-flex mb-3 row p-2">
              <div class="col-5">
                <label for="name" class="form-label">
                  <p class="serif-font mb-0">Name of Ingredient</p>
                </label>
                <input v-model="editableIngredient.name" type="text" class="form-control" id="name" placeholder="Cinnamon"
                  required maxlength="255" minlength="3">
              </div>
              <div class="col-6">
                <label for="quantity" class="form-label">
                  <p class="serif-font mb-0">Quantity of Ingredient</p>
                </label>
                <input v-model="editableIngredient.quantity" type="text" class="form-control" id="quantity"
                  placeholder="1 tsp" required maxlength="255" minlength="1">
              </div>
            </div>
            <div v-else>
              <form @submit.prevent="editIngredients()" id="edit-ingredients">
                <div class="row p-2 p-md-0">
                  <div class="col-5">
                    <label for="name" class="form-label">
                      <p class="serif-font mb-0">Name of Ingredient</p>
                    </label>
                  </div>
                  <div class="col-6">
                    <label for="quantity" class="form-label">
                      <p class="serif-font mb-0">Quantity of Ingredient</p>
                    </label>
                  </div>
                </div>
                <div v-for="ingredient in editableIngredients" :key="ingredient.id" class="row mb-2">
                  <div class="col-5">
                    <input v-model="ingredient.name" type="text" class="form-control" id="name" required maxLength="255"
                      minLength="3">
                  </div>
                  <div class="col-6">
                    <input v-model="ingredient.quantity" type="text" class="form-control" id="name" required
                      maxLength="255" minLength="1">
                  </div>
                </div>
              </form>
            </div>
          </div>
          <div v-if="addingIngredients" class="d-flex">
            <button @click="flipIngredientTextarea" class="btn btn-theme-orange me-2">Finished</button>
            <button @click="addIngredient" class="btn btn-theme-green fs-5">Add Ingredient</button>
          </div>
          <div v-else-if="editingIngredients" class="d-flex">
            <button @click="reloadEditableIngredients" class="btn btn-theme-orange p-2 me-3 me-md-1">Cancel
              Changes</button>
            <button type="submit" form="edit-ingredients" class="btn btn-theme-green p-2">Submit Changes</button>
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
import { favoritesService } from "../services/FavoritesService.js";
import { Modal } from "bootstrap";


export default {
  setup() {
    let addingInstructions = ref(false);
    let addingIngredients = ref(false);
    let editingIngredients = ref(false);
    let editableInstructions = ref("")
    let editableIngredient = ref({})
    let editableIngredients = ref([])

    onMounted(() => {
      let recipeCardModalElem = document.getElementById('recipeCardModal')
      recipeCardModalElem.addEventListener('hidden.bs.modal', function (event) {
        resetAll()
        editableIngredients.value = JSON.parse(JSON.stringify(AppState.activeRecipeIngredients));
      })
    })

    watchEffect(() => {
      if (AppState.activeRecipe) {
        if (AppState.activeRecipe.instructions) {
          editableInstructions.value = (AppState.activeRecipe.instructions)
        } else {
          editableInstructions.value = ""
        }
      }
      else {
        editableInstructions.value = ""
      }
      if (AppState.activeRecipeIngredients.length) {
        editableIngredients.value = JSON.parse(JSON.stringify(AppState.activeRecipeIngredients));
      }
    })

    function resetAll() {
      addingInstructions.value = false
      addingIngredients.value = false
      editingIngredients.value = false
    }

    return {
      editableInstructions,
      editableIngredient,
      editableIngredients,
      addingInstructions,
      addingIngredients,
      editingIngredients,
      ingredientsAreHidden: computed(() => AppState.ingredientsAreHidden),
      account: computed(() => AppState.account),
      activeRecipe: computed(() => AppState.activeRecipe),
      activeRecipeIngredients: computed(() => AppState.activeRecipeIngredients),
      isFavRecipe: computed(() => AppState.myFavoriteRecipes.find((recipe) => recipe.id == AppState.activeRecipe.id)),

      flipInstructionTextarea() {
        addingInstructions.value = !addingInstructions.value;
      },

      flipIngredientTextarea() {
        addingIngredients.value = !addingIngredients.value;
      },

      flipEditIngredientsForm() {
        editingIngredients.value = !editingIngredients.value;
      },

      reloadEditableInstructions() {
        this.flipInstructionTextarea()
        editableInstructions.value = (AppState.activeRecipe.instructions)
      },

      reloadEditableIngredients() {
        try {
          this.flipEditIngredientsForm()
          editableIngredients.value = JSON.parse(JSON.stringify(AppState.activeRecipeIngredients));
        } catch (error) {
          Pop.error(error)
        }
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
          await ingredientsService.addIngredient(ingredient)
          editableIngredient.value = {}
          Pop.success("Ingredient added!")
        } catch (error) {
          Pop.error(error)
        }
      },

      async editIngredients() {
        try {
          let ingredients = editableIngredients.value
          editingIngredients.value = false
          await ingredientsService.editIngredients(ingredients)
          editableIngredients.value = JSON.parse(JSON.stringify(AppState.activeRecipeIngredients));
          Pop.success("Ingredients edited!")
        } catch (error) {
          Pop.error(error)
        }
      },

      async createFavorite(recipeId) {
        try {
          await favoritesService.createFavorite(recipeId)
          Pop.success("You have favorited this recipe.")
        } catch (error) {
          Pop.error(error)
        }
      },

      async removeFavorite(recipeId) {
        try {
          await favoritesService.removeFavorite(recipeId)
          Pop.success("You have unfavorited this recipe.")
        } catch (error) {
          Pop.error(error)
        }
      },

      async removeRecipe(recipeId) {
        try {
          let wantsToDelete = await Pop.confirm("Are you sure you want to delete this recipe?")
          if (!wantsToDelete) {
            return
          }
          await recipesService.removeRecipe(recipeId)
          Pop.success("Recipe deleted.")
          Modal.getOrCreateInstance('#recipeCardModal').hide()
        } catch (error) {
          Pop.error(error)
        }
      }




    }
  }
};
</script>


<style lang="scss" scoped>
textarea {
  height: 35vh;
}

input {
  width: 11rem;
}

.reci-img {
  height: 28rem;
  width: 100%;
  object-fit: cover;
}

.reci-card {
  height: 100%;
}

.recipeCard-words {
  position: absolute;
  background-color: rgba(82, 69, 69, 0.12);
  backdrop-filter: blur(13px);
  font-weight: bold;
  left: 82%;
  top: 0%;
}

.delete-recipe-button {
  position: absolute;
  left: 5%;
  top: 90%;
}

.invisible-on-desktop {
  display: flex;
}

.invisible-on-mobile {
  display: flex;
}


//below styles apply to md screens and below
@media screen and (min-width: 768px) {
  .invisible-on-desktop {
    display: none;
  }
}

//below styles apply to screens smaller than md
@media screen and (max-width: 768px) {
  .invisible-on-mobile {
    display: none;
  }

  input {
    width: 8rem;
  }
}
</style>
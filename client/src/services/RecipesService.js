import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService {

  async getRecipes() {
    const res = await api.get("api/recipes")
    AppState.recipes = res.data.map(recipePOJO => new Recipe(recipePOJO))
  }

  setActiveRecipe(recipeId) {
    AppState.activeRecipe = AppState.recipes.find(recipe => recipe.id == recipeId)
    this.findIngredients()
  }

  async findIngredients() {
    let recipeId = AppState.activeRecipe.id
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    AppState.activeRecipeIngredients = res.data.map(pojo => new Ingredient(pojo))
  }

  async createRecipe(recipeData) {
    const res = await api.post("api/recipes", recipeData)
    let newRecipe = new Recipe(res.data)
    AppState.recipes.push(newRecipe)
    AppState.activeRecipe = newRecipe
  }

  async addInstructions(instructions) {
    let activeRecipe = AppState.activeRecipe;
    if (!activeRecipe) {
      return
    }
    AppState.activeRecipe.instructions = instructions
    const res = await api.put(`api/recipes/${activeRecipe.id}`, activeRecipe)
    let updatedRecipe = new Recipe(res.data)
    AppState.activeRecipe = updatedRecipe
  }

  async removeRecipe(recipeId) {
    let res = await api.delete(`api/recipes/${recipeId}`)
    let recipeIndex = AppState.recipes.findIndex(recipe => recipe.id == recipeId)
    AppState.recipes.splice(recipeIndex, 1)
  }

  clearAppState() {
    AppState.activeRecipe = null
    AppState.activeRecipeIngredients.length = 0
  }


}

export const recipesService = new RecipesService();
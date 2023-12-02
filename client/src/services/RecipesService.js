import { AppState } from "../AppState.js";
import { Ingredient } from "../models/Ingredient.js";
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class RecipesService {

  async getRecipes() {
    const res = await api.get("api/recipes")
    logger.log("Recipes from back end", res.data)
    AppState.recipes = res.data.map(recipePOJO => new Recipe(recipePOJO))
    logger.log("AppState Recipes", AppState.recipes)
  }

  setActiveRecipe(recipeId) {
    AppState.activeRecipe = AppState.recipes.find(recipe => recipe.id == recipeId)
    this.findIngredients()
  }

  async findIngredients() {
    let recipeId = AppState.activeRecipe.id
    const res = await api.get(`api/recipes/${recipeId}/ingredients`)
    AppState.activeRecipeIngredients = res.data.map(pojo => new Ingredient(pojo))
    logger.log("Active recipe ingredients", AppState.activeRecipeIngredients)
  }

  async createRecipe(recipeData) {
    const res = await api.post("api/recipes", recipeData)
    logger.log('CREATED Recipe', res.data)
    let newRecipe = new Recipe(res.data)
    AppState.recipes.push(newRecipe)
    AppState.activeRecipe = newRecipe
  }

  async addInstructions(instructions) {
    let activeRecipe = AppState.activeRecipe;
    if (!activeRecipe) {
      return
    }
    //check the above
    logger.log("before adding", AppState.activeRecipe.instructions)
    AppState.activeRecipe.instructions = instructions
    logger.log("after adding", AppState.activeRecipe.instructions)
    const res = await api.put(`api/recipes/${activeRecipe.id}`, activeRecipe)
    logger.log('EDITED Recipe', res.data)
    let updatedRecipe = new Recipe(res.data)
    AppState.activeRecipe = updatedRecipe
  }

  clearAppState() {
    AppState.activeRecipe = null
    AppState.activeRecipeIngredients.length = 0
  }


}

export const recipesService = new RecipesService();
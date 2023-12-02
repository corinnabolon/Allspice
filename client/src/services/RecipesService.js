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

  clearAppState() {
    AppState.activeRecipe = null
    AppState.activeRecipeIngredients.length = 0
    AppState.addingInstructions = false
    logger.log("Adding instructions set to false")
  }


}

export const recipesService = new RecipesService();
import { AppState } from "../AppState.js";
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
    AppState.activeRecipe = AppState.recipes.filter(recipe => recipe.id == recipeId)
  }


}

export const recipesService = new RecipesService();
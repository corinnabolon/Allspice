import { AppState } from '../AppState';
import { Ingredient } from "../models/Ingredient.js";
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class IngredientsService {

  async addIngredient(ingredient) {
    ingredient.recipeId = AppState.activeRecipe.id
    const res = await api.post("api/ingredients", ingredient)
    AppState.activeRecipeIngredients.push(new Ingredient(res.data))
  }

  async editIngredients(ingredients) {
    logger.log("ingredients", ingredients)
    for (const ingredient of ingredients) {
      await api.put(`api/ingredients/${ingredient.id}`, ingredient)
    }
  }

  async removeIngredient(ingredientId) {
    await api.delete(`api/ingredients/${ingredientId}`)
    AppState.activeRecipeIngredients = AppState.activeRecipeIngredients.filter(
      (ingredient) => ingredient.id != ingredientId)
  }

}

export const ingredientsService = new IngredientsService();
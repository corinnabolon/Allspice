import { AppState } from '../AppState';
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class IngredientsService {

  async addIngredient(ingredient) {
    ingredient.recipeId = AppState.activeRecipe.id
    logger.log(ingredient)
    const res = await api.post("api/ingredients", ingredient)
    recipesService.findIngredients()
  }

  async editIngredients(ingredients) {
    logger.log("ingredients", ingredients)
    for (const ingredient of ingredients) {
      await api.put(`api/ingredients/${ingredient.id}`, ingredient)
    }
    recipesService.findIngredients()
  }

  async removeIngredient(ingredientId) {
    await api.delete(`api/ingredients/${ingredientId}`)
    recipesService.findIngredients()
  }

}

export const ingredientsService = new IngredientsService();
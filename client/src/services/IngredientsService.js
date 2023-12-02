import { AppState } from '../AppState';
import { recipesService } from "../services/RecipesService.js"
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class IngredientsService {

  async addIngredient(ingredient) {
    ingredient.recipeId = AppState.activeRecipe.id
    logger.log(ingredient)
    const res = await api.post("api/ingredients", ingredient)
    AppState.activeRecipeIngredients.push(new Ingredient(res.data))
  }

  async editIngredients(ingredients) {
    logger.log("ingredients", ingredients)
    for (const ingredient of ingredients) {
      await api.put(`api/ingredients/${ingredient.id}`, ingredient)
      let index = AppState.activeRecipeIngredients.findIndex(ingredient)
      AppState.activeRecipeIngredients.splice(index, 1, ingredient)
    }
  }

  async removeIngredient(ingredientId) {
    await api.delete(`api/ingredients/${ingredientId}`)
    AppState.activeRecipeIngredients = AppState.activeRecipeIngredients.filter(
      (ingredient) => ingredient.id != ingredientId)
  }

}

export const ingredientsService = new IngredientsService();
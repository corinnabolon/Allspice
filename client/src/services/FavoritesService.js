import { AppState } from "../AppState.js"
import { FavoritedRecipe } from "../models/FavoritedRecipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class FavoritesService {

  async createFavorite(recipeId) {
    let foundRecipe = AppState.recipes.find(recipe => recipe.id == recipeId)
    foundRecipe.recipeId = foundRecipe.id
    const res = await api.post("api/favorites", foundRecipe)
    foundRecipe.accountId = res.data.accountId
    foundRecipe.favoriteId = res.data.id
    let newFavorite = new FavoritedRecipe(foundRecipe)
    AppState.myFavoriteRecipes.push(newFavorite)
  }

  async removeFavorite(recipeId) {
    let favoriteToDelete = AppState.myFavoriteRecipes.find(favorite => favorite.id == recipeId)
    if (!favoriteToDelete) {
      return
    }
    let favoriteId = favoriteToDelete.favoriteId
    const res = await api.delete(`api/favorites/${favoriteId}`)
    let index = AppState.myFavoriteRecipes.findIndex(favorite => favorite.id == recipeId)
    AppState.myFavoriteRecipes.splice(index, 1)
  }


}

export const favoritesService = new FavoritesService();
import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { logger } from "../utils/Logger.js"
import { accountService } from "./AccountService.js"
import { api } from "./AxiosService.js"



class FavoritesService {

  async createFavorite(recipeId) {
    const res = await api.post("api/favorites", { recipeId })
    logger.log('Favorited recipe', res.data)
    let newFavorite = new Favorite(res.data)
    let favoritedRecipe = AppState.recipes.find(recipe => recipe.id == newFavorite.recipeId)
    AppState.myFavoriteRecipes.push(favoritedRecipe)
  }

  async deleteFavorite(recipeId) {
    let favoriteToDelete = myFavorites.find(favorite => favorite.recipeId == recipeId)
    if (!favoriteToDelete) {
      return
    }
    let unfavoriteId = recipeToUnfavorite.id
    logger.log("unfavoriteId", unfavoriteId)
    // const res = await api.delete(`api/favorites/${unfavoriteId}`)
    let unfavorited = res.data
    logger.log("unfavorited", unfavorited)
  }


}

export const favoritesService = new FavoritesService();
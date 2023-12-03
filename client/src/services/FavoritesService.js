import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { FavoritedRecipe } from "../models/FavoritedRecipe.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { accountService } from "./AccountService.js"
import { api } from "./AxiosService.js"



class FavoritesService {

  async createFavorite(recipeId) {
    let foundRecipe = AppState.recipes.find(recipe => recipe.id == recipeId)
    logger.log("Found recipe", foundRecipe)
    foundRecipe.recipeId = foundRecipe.id
    const res = await api.post("api/favorites", foundRecipe)
    logger.log('Favorited recipe', res.data)
    foundRecipe.accountId = res.data.accountId
    foundRecipe.favoriteId = res.data.id
    let newFavorite = new FavoritedRecipe(foundRecipe)
    logger.log("As a favorited Recipe", newFavorite)
    // let favoritedRecipe = AppState.recipes.find(recipe => recipe.id == newFavorite.recipeId)
    // AppState.myFavoriteRecipes.push(favoritedRecipe)
    AppState.myFavoriteRecipes.push(newFavorite)
    logger.log("AppState.myFavoritedRecipes", AppState.myFavoriteRecipes)
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
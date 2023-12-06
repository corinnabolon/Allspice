import { AppState } from "../AppState.js"
import { FavoritedRecipe } from "../models/FavoritedRecipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"



class FavoritesService {


  //Note: recipeId == a recipe.id that we clicked on; we turn it into a recipeId because CreateFavorite on the back end creates a many-to-many with an accountId and a recipeId, so we have to post / create it in that format
  async createFavorite(recipeId) {
    let foundRecipe = AppState.recipes.find(recipe => recipe.id == recipeId)
    foundRecipe.recipeId = foundRecipe.id
    //requires a recipeId to be posted to favorites
    const res = await api.post("api/favorites", foundRecipe)
    foundRecipe.accountId = res.data.accountId
    foundRecipe.favoriteId = res.data.id
    //favoriteId is the ID of the many-to-many itself--what we will need to delete it--whereas the foundRecipe.id matches the ID of the recipe which the FavoritedRecipe we make extends from
    let newFavorite = new FavoritedRecipe(foundRecipe)
    AppState.myFavoriteRecipes.push(newFavorite)
  }

  async removeFavorite(recipeId) {
    let favoriteToDelete = AppState.myFavoriteRecipes.find(favorite => favorite.id == recipeId)
    //id of the associated favorite should match the id of the recipe
    if (!favoriteToDelete) {
      return
    }
    let favoriteId = favoriteToDelete.favoriteId
    //favoriteId is what we need to pass through to the URL to delete it (the many-to-many)
    const res = await api.delete(`api/favorites/${favoriteId}`)
    let index = AppState.myFavoriteRecipes.findIndex(favorite => favorite.id == recipeId)
    //the favorite.id and recipe.id will match
    AppState.myFavoriteRecipes.splice(index, 1)
  }


}

export const favoritesService = new FavoritesService();
import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { FavoritedRecipe } from "../models/FavoritedRecipe.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getMyFavoritedRecipes() {
    const res = await api.get("/account/favorites")
    AppState.myFavoriteRecipes = res.data.map(POJO => new FavoritedRecipe(POJO))
  }

  async editAccount(data) {
    const res = await api.put("/account", data)
    AppState.account = new Account(res.data)
  }
}

export const accountService = new AccountService()

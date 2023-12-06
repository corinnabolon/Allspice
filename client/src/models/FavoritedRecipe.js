import { Recipe } from "./Recipe.js"


export class FavoritedRecipe extends Recipe {
  constructor(data) {
    super(data);
    this.favoriteId = data.favoriteId
    this.accountId = data.accountId
  }
}
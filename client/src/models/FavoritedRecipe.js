import { Recipe } from "./Recipe.js"


export class FavoritedRecipe extends Recipe {
  constructor(data) {
    super(data);
    this.favoriteId = data.favoriteId
    this.accountId = data.accountId
  }
}

const Recipe = `this.id = data.id
this.createdAt = data.createdAt
this.updatedAt = data.updatedAt
this.title = data.title
this.instructions = data.instructions || null
this.img = data.img
this.category = data.category
this.creatorId = data.creatorId
this.creator = data.creator`
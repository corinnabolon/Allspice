export class Recipe {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.CreatedAt)
    this.updatedAt = new Date(data.UpdatedAt)
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}
namespace Allspice.Repositories;

public class RecipesRepository
{

  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }


  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"INSERT INTO recipes(title,
        instructions,
        img,
        category,
        creatorId) VALUES(@Title, @Instructions, @Img, @Category, @creatorId);
        
            SELECT 
    reci.*,
    acc.*
    FROM recipes reci
    JOIN accounts acc ON reci.creatorId = acc.id
    WHERE reci.id = LAST_INSERT_ID();";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, recipeData).FirstOrDefault();

    return recipe;
  }

  internal List<Recipe> GetRecipes()
  {
    string sql = @"SELECT 
    reci.*,
    acc.* 
    FROM recipes reci
    JOIN accounts acc ON reci.creatorId = acc.id;";

    List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }).ToList();

    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT reci.*, acc.*
FROM recipes reci
    JOIN accounts acc ON reci.creatorId = acc.id
WHERE reci.id = @recipeId;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { recipeId }).FirstOrDefault();

    return recipe;
  }

  internal Recipe UpdateRecipe(int recipeId, Recipe recipeData)
  {
    string sql = @"
    UPDATE recipes
    SET
    title = @Title,
    instructions = @Instructions,
    img = @Img,
    category = @Category
    WHERE id = @recipeId;
    
    SELECT
    reci.*,
    acc.*
    FROM recipes reci
    JOIN accounts acc ON acc.id = reci.creatorId
    WHERE reci.id = @recipeId;";

    Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, recipeData).FirstOrDefault();
    return recipe;
  }

}
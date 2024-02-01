


namespace Allspice.Repositories;

public class FavoritesRepository
{

  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites(recipeId, accountId) VALUES (@RecipeId, @AccountId);

    SELECT * FROM favorites WHERE id = LAST_INSERT_ID();";

    Favorite favorite = _db.Query<Favorite>(sql, favoriteData).FirstOrDefault();

    return favorite;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    string sql = @"SELECT * FROM favorites WHERE id = @favoriteId;";

    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();

    return favorite;
  }

  internal List<FavoritedRecipe> GetRecipeFavoritesByAccountId(string userId)
  {
    string sql = @"
    SELECT
    fav.*,
    reci.*,
    acc.*
    FROM favorites fav
    JOIN recipes reci ON fav.recipeId = reci.id
    JOIN accounts acc ON acc.id = reci.creatorId
    WHERE fav.accountId = @userId;";

    //Get the favorites table, which is the many-to-many with just a recipeId and accountId on it
    //Get the recipe table so we can connect all of the relevant recipe data to it
    //Get the account table to we can get the recipe's creatorId
    //Where the favorite's accountId is the user ID

    List<FavoritedRecipe> favoritedRecipes = _db.Query<Favorite, FavoritedRecipe, Profile, FavoritedRecipe>(sql, (favorite, favoritedRecipe, profile) =>
    {
      favoritedRecipe.FavoriteId = favorite.Id;
      favoritedRecipe.AccountId = favorite.AccountId;
      favoritedRecipe.Creator = profile;
      return favoritedRecipe;
    }, new { userId }).ToList();
    return favoritedRecipes;
  }

  //    fav.*, reci.*, acc.* in the SQL is the sane order as <Favorite, FavoriteRecipe, Profile, (FavoritedRecipe--the return type)> and the parameters of the unnamed function (favorite, favoritedRecipe, profile) which we pass through so that we can assign the additional properties that are needed to make the recipe into a FavoritedRecipe, which is an object which extends recipe and adds the FavoriteId and AccountId (favorite.AccountId) and populates the Creator as the profile
  //Making it a FavoritedRecipe and not just a favorite makes it easier to work with and render on the front end because it has all the properties of a recipe

  internal void RemoveFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";
    _db.Execute(sql, new { favoriteId });
  }
}
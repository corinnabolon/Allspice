


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

    List<FavoritedRecipe> favoritedRecipes = _db.Query<Favorite, FavoritedRecipe, Profile, FavoritedRecipe>(sql, (favorite, favoritedRecipe, profile) =>
    {
      favoritedRecipe.FavoriteId = favorite.Id;
      favoritedRecipe.AccountId = favorite.AccountId;
      favoritedRecipe.Creator = profile;
      return favoritedRecipe;
    }, new { userId }).ToList();
    return favoritedRecipes;
  }

  internal void RemoveFavorite(int favoriteId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";
    _db.Execute(sql, new { favoriteId });
  }
}
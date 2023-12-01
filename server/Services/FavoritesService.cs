


namespace Allspice.Services;

public class FavoritesService
{

  private readonly FavoritesRepository _repository;

  public FavoritesService(FavoritesRepository repository)
  {
    _repository = repository;
  }

  internal Favorite CreateFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repository.CreateFavorite(favoriteData);
    return favorite;
  }

  internal List<FavoritedRecipe> GetRecipeFavoritesByAccountId(string userId)
  {
    List<FavoritedRecipe> favoritedRecipe = _repository.GetRecipeFavoritesByAccountId(userId);
    return favoritedRecipe;
  }

  internal Favorite GetFavoriteById(int favoriteId)
  {
    Favorite favorite = _repository.GetFavoriteById(favoriteId);
    if (favorite == null)
    {
      throw new Exception($"{favoriteId} is not a valid favorite ID");
    }
    return favorite;
  }

  internal string RemoveFavorite(int favoriteId, string userId)
  {
    Favorite favorite = GetFavoriteById(favoriteId);
    if (favorite.AccountId != userId)
    {
      throw new Exception($"Not your favorite to delete!");
    }
    _repository.RemoveFavorite(favoriteId);
    return "Favorite deleted!";
  }
}
namespace Allspice.Models;

public class FavoritedRecipe : Recipe
{
  public int FavoriteId { get; set; }

  public string AccountId { get; set; }
}
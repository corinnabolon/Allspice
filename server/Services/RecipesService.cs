namespace Allspice.Services;

public class RecipesService
{

  private readonly RecipesRepository _repository;

  public RecipesService(RecipesRepository recipesRepository)
  {
    _repository = recipesRepository;
  }


  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repository.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetRecipes()
  {
    List<Recipe> recipes = _repository.GetRecipes();
    return recipes;
  }



}
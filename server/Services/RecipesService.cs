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


  internal List<Recipe> GetRecipes(string category)
  {
    if (category == null)
    {
      List<Recipe> recipes = _repository.GetRecipes();
      return recipes;
    }

    List<Recipe> recipesWithQuery = _repository.GetRecipesWithQuery(category);
    return recipesWithQuery;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repository.GetRecipeById(recipeId);

    if (recipe == null)
    {
      throw new Exception($"No recipe with the ID {recipeId}");
    }

    return recipe;
  }

  internal Recipe UpdateRecipe(string userId, int recipeId, Recipe recipeData)
  {
    Recipe originalRecipe = GetRecipeById(recipeId);

    if (originalRecipe.CreatorId != userId)
    {
      throw new Exception("Not your recipe to edit!");
    }

    originalRecipe.Title = recipeData.Title ?? originalRecipe.Title;
    originalRecipe.Instructions = recipeData.Instructions ?? originalRecipe.Instructions;
    originalRecipe.Img = recipeData.Img ?? originalRecipe.Img;
    originalRecipe.Category = recipeData.Category ?? originalRecipe.Category;

    _repository.UpdateRecipe(recipeId, originalRecipe);

    return originalRecipe;
  }

  internal string RemoveRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);

    if (recipe.CreatorId != userId)
    {
      throw new Exception("Not your recipe to delete!");
    }

    _repository.RemoveRecipe(recipeId);

    return $"Recipe with recipe Id {recipeId} has been deleted!";
  }



}



using System.Runtime.CompilerServices;

namespace Allspice.Services;

public class IngredientsService
{

  private readonly IngredientsRepository _repository;
  private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepository repository, RecipesService recipesService)
  {
    _repository = repository;
    _recipesService = recipesService;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData, string userId)
  {
    Recipe recipe = _recipesService.GetRecipeById(ingredientData.RecipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("This is not your recipe, so you can't create ingredients for it.");
    }
    Ingredient ingredient = _repository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repository.GetIngredientsByRecipeId(recipeId);
    return (ingredients);
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _repository.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception($"No ingredient with the id {ingredientId}");
    }
    return ingredient;
  }

  internal string RemoveIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetIngredientById(ingredientId);
    if (ingredient.CreatorId != userId)
    {
      throw new Exception("Not your ingredient to delete!");
    }
    _repository.RemoveIngredient(ingredientId);
    return "Ingredient was deleted!";
  }

  internal Ingredient UpdateIngredient(string userId, int ingredientId, Ingredient ingredientData)
  {
    Ingredient originalIngredient = GetIngredientById(ingredientId);

    if (originalIngredient.CreatorId != userId)
    {
      throw new Exception("Not your ingredient to delete!");
    }

    originalIngredient.Name = ingredientData.Name ?? originalIngredient.Name;
    originalIngredient.Quantity = ingredientData.Quantity ?? originalIngredient.Quantity;

    _repository.UpdateIngredient(ingredientId, originalIngredient);

    return originalIngredient;
  }

}
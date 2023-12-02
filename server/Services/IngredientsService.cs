


namespace Allspice.Services;

public class IngredientsService
{

  private readonly IngredientsRepository _repository;

  public IngredientsService(IngredientsRepository repository)
  {
    _repository = repository;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
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

  internal string RemoveIngredient(int ingredientId)
  {
    _repository.RemoveIngredient(ingredientId);
    return "Ingredient was deleted!";
  }

  internal Ingredient UpdateIngredient(string userId, int ingredientId, Ingredient ingredientData)
  {
    Ingredient originalIngredient = GetIngredientById(ingredientId);

    originalIngredient.Name = ingredientData.Name ?? originalIngredient.Name;
    originalIngredient.Quantity = ingredientData.Quantity ?? originalIngredient.Quantity;

    _repository.UpdateIngredient(ingredientId, originalIngredient);

    return originalIngredient;
  }

}
namespace Allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientsController : ControllerBase
{

  private readonly IngredientsService _ingredientsService;

  public IngredientsController(IngredientsService ingredientsService)
  {
    _ingredientsService = ingredientsService;
  }


  [HttpPost]

  public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ingredientId}")]

  public ActionResult<string> RemoveIngredient(int ingredientId)
  {
    try
    {
      String message = _ingredientsService.RemoveIngredient(ingredientId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }


  }





}
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class IngredientsController : ControllerBase
{

  private readonly IngredientsService _ingredientsService;

  private readonly Auth0Provider _auth0Provider;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet("{ingredientId}")]

  public ActionResult<Ingredient> GetIngredientById(int ingredientId)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.GetIngredientById(ingredientId);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]

  async public Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      ingredientData.CreatorId = userInfo.Id;
      string userId = userInfo.Id;
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData, userId);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPut("{ingredientId}")]
  public async Task<ActionResult<Ingredient>> UpdateIngredient(int ingredientId, [FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      Ingredient ingredient = _ingredientsService.UpdateIngredient(userId, ingredientId, ingredientData);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




  [Authorize]
  [HttpDelete("{ingredientId}")]

  public async Task<ActionResult<string>> RemoveIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo.Id;
      String message = _ingredientsService.RemoveIngredient(ingredientId, userId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }


  }





}
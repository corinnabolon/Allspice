namespace Allspice.Models;

public class Recipe
{
  public int Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public string Title { get; set; }

  public string Instructions { get; set; }

  public string Img { get; set; }

  public string Category { get; set; }

  public string CreatorId { get; set; }

  public Profile Creator { get; set; }
}

// private DateTime JSONdateTime2dateTime(long JSONdatetimelong)
// {
//     long DT_Tic = (JSONdatetimelong + 62135607600000) * 10000;
//     return new DateTime(DT_Tic);
// }
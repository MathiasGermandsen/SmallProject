using System.ComponentModel.DataAnnotations;

namespace SmallProject.Data;

public class CatFact
{
  [Key]
  public string Fact { get; set; } = string.Empty;
  public DateTime DateAdded { get; set; }
  public int Likes { get; set; } = 0;
  public int Dislikes { get; set; } = 0;
}

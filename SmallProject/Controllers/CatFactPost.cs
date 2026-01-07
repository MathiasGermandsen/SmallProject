using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallProject.Data;

namespace SmallProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatFactController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public CatFactController(ApplicationDbContext context)
  {
    _context = context;
  }

  [HttpPost("like/{fact}")]
  public async Task<IActionResult> Like(string fact)
  {
    var catFact = await _context.CatFacts.FindAsync(fact);
    if (catFact == null)
    {
      catFact = new CatFact
      {
        Fact = fact,
        DateAdded = DateTime.UtcNow,
        Likes = 1,
        Dislikes = 0
      };
      _context.CatFacts.Add(catFact);
    }

    catFact.Likes++;
    await _context.SaveChangesAsync();

    return Ok(new { fact = catFact.Fact, likes = catFact.Likes, dislikes = catFact.Dislikes });
  }

  [HttpPost("dislike/{fact}")]
  public async Task<IActionResult> Dislike(string fact)
  {
    var catFact = await _context.CatFacts.FindAsync(fact);
    if (catFact == null)
    {
      catFact = new CatFact
      {
        Fact = fact,
        DateAdded = DateTime.UtcNow,
        Likes = 0,
        Dislikes = 1
      };
      _context.CatFacts.Add(catFact);
    }

    catFact.Dislikes++;
    await _context.SaveChangesAsync();

    return Ok(new { fact = catFact.Fact, likes = catFact.Likes, dislikes = catFact.Dislikes });
  }
}

public class CatFactRequest
{
  public string Fact { get; set; } = string.Empty;
}

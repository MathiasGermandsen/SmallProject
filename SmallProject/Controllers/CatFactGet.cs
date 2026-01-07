using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallProject.Data;

namespace SmallProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatFactGetController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  private readonly ILogger<CatFactGetController> _logger;

  public CatFactGetController(ApplicationDbContext context, ILogger<CatFactGetController> logger)
  {
    _context = context;
    _logger = logger;
  }

  [HttpGet]
  public async Task<ActionResult<object>> GetAllCatFacts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
  {
    try
    {
      if (page < 1) page = 1;
      if (pageSize < 1 || pageSize > 100) pageSize = 10;

      var totalCount = await _context.CatFacts.CountAsync();
      var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

      var catFacts = await _context.CatFacts
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return Ok(new
      {
        Data = catFacts,
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount,
        TotalPages = totalPages
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error retrieving cat facts");
      return StatusCode(500, "An error occurred while retrieving cat facts");
    }
  }
}

using Glovo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfoController : Controller
{
    private readonly GlovoDbContext _context;

    public InfoController(GlovoDbContext context)
    {
        _context = context;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> Roles()
    {
        return Ok(await _context.Roles.ToListAsync());
    }

    [HttpGet("categories")]
    public async Task<IActionResult> Categories()
    {
        return Ok(await _context.Categories.ToListAsync());
    }

    [HttpGet("parameters")]
    public IActionResult Parameters(int id, string name)
    {
        return Ok(new
        {
            Id = id,
            Name = name
        });
    }
}
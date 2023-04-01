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

    [HttpGet]
    public async Task<IActionResult> Roles()
    {
        var a = "";

        return Ok(await _context.Roles.ToListAsync());
    }
}
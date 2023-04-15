using Companies.Models;
using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers.Company;

[Route("api/company/[controller]")]
[ApiController]
[Authorize(Roles = "Company,Moderator")]
public class OrderController : Controller
{
    private readonly GlovoDbContext _context;

    public OrderController(GlovoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Orders.OrderBy(x => x.IsConfirm).ToListAsync());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Order order)
    {
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok();
    }
}

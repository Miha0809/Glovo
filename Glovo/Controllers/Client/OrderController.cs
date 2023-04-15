using System.Security.Claims;
using Companies.Models;
using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers.Client;

[Route("api/client/[controller]")]
[ApiController]
[Authorize(Roles = "Client,Moderator")]
public class OrderController : ControllerBase
{
    private readonly GlovoDbContext _context;

    public OrderController(GlovoDbContext context)
    {
        _context = context;
    }

    [HttpGet("delayed")]
    public async Task<IActionResult> GetDelayed()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);
        return Ok(await _context.Orders.Where(order => !order.IsConfirm && order.Client.Id.Equals(id)).ToListAsync());
    }

    [HttpGet("accepting")]
    public async Task<IActionResult> GetAccepting()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);
        return Ok(await _context.Orders.Where(order => order.IsConfirm && order.Client.Id.Equals(id)).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Order order)
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);

        order.Client = await _context.Clients.FirstAsync(client => client.Id.Equals(id));

        await _context.AddAsync(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        order.Id = id;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return Ok(await _context.Orders.FindAsync(id));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        _context.Orders.Remove(order!);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

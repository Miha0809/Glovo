using System.Security.Claims;
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
        try
        {
            int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);
            var company = await _context.Companies.FindAsync(id);
        
            // TODO: шукати свої не підтверджені замовлення
            var vs = await _context.Orders.AsAsyncEnumerable().Where((order, index) => !order.IsConfirm && order.Products[index].CompanyName.Equals(company.Name)).ToListAsync();
        
            return Ok(new
            {
                One = await _context.Orders.Where(order => !order.IsConfirm).ToListAsync(),
                Two = vs
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Confirm(int id, [FromBody] Order order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        order.Id = id;
        order.IsConfirm = true;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        
        return Ok(await _context.Orders.FindAsync(id));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order is null)
        {
            return BadRequest();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}

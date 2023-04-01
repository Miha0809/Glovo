using Companies.Models;
using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers.Company;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Companies,Moderator")]
public class ProductController : Controller
{
    private readonly GlovoDbContext _context;

    public ProductController(GlovoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Products.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Update(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = _context.Products.FirstOrDefault(product => product.Id.Equals(id));
        
        _context.Products.Remove(product!);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

using System.Security.Claims;
using Company.Models;
using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers.Company;

[Route("api/company/[controller]")]
[ApiController]
[Authorize(Roles = "Company,Moderator")]
public class ProductController : Controller
{
    private readonly GlovoDbContext _context;

    public ProductController(GlovoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Products.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Product product)
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        product.Company ??= await _context.Companies.FindAsync(id);
        
        if (await _context.Categories.FirstOrDefaultAsync(category => category.Name.Equals(product.Category.Name)) ==
            null)
        {
            // TODO: if it does not have a category in the database, then send it to the moderator for review
            _context.Categories.Add(product.Category);
        }

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        // TODO: Redirect to moderator
        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        product.Id = id;
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return Ok(await _context.Products.FindAsync(id));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(product => product.Id.Equals(id));
        
        _context.Products.Remove(product!);
        await _context.SaveChangesAsync();

        return Ok();
    }
}

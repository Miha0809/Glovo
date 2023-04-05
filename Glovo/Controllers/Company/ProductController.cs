using Companies.Models;
using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers.Company;

[Route("api/[controller]")]
[ApiController]
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
    [Authorize(Roles = "Company,Moderator")]
    public async Task<IActionResult> Add([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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
    [Authorize(Roles = "Company,Moderator")]
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
    [Authorize(Roles = "Company,Moderator")]
    public async Task<IActionResult> Delete(int id)
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

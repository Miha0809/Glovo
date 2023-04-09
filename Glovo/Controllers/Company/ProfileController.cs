using System.Security.Claims;
using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Controllers.Company;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Company")]
public class ProfileController : ControllerBase
{
    private readonly GlovoDbContext _context;

    public ProfileController(GlovoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);
        return Ok(await _context.Companies.FindAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Companies.Models.Company company)
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);

        company.Id = id;
        company.Password = Hash.GetHash(company.Password);
        company.Email ??= await _context.Emails.FirstOrDefaultAsync(email =>
                email.Name.Equals(HttpContext.User.FindFirstValue("Email")));
        company.Role =
            (await _context.Roles.FirstOrDefaultAsync(role =>
                role.Name.Equals(HttpContext.User.FindFirstValue("Role"))))!;
        company.Products = _context.Companies
            .FirstOrDefaultAsync(x => x.Id.Equals(HttpContext.User.FindFirstValue("Id"))).Result?.Products;
        
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();

        return Ok(await _context.Companies.FindAsync(id));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);

        var company = await _context.Companies.FindAsync(id);

        if (company != null)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Ok();
        }

        return BadRequest();
    }
}
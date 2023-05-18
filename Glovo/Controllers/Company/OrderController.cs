using System.Security.Claims;
using AutoMapper;
using Company.Models.Dtos;
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
    private readonly IMapper _mapper;
    public OrderController(GlovoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("not_confirms")]
    public async Task<IActionResult> GetNotConfirms()
    {
        try
        {
            int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);

            var company = await _context.Companies.FindAsync(id);
            var orders = await _context.Orders
                .Where(order => !order.IsConfirm && order.Products.Any(product =>
                    product.Company != null && product.Company.Name.Equals(company.Name)))
                .ToListAsync();

            // TODO: Add address

            return Ok(orders.Select(order => new
                {
                    order.Id,
                    order.Date,
                    order.IsConfirm,
                    ClientName = order.Client.Name,
                    Products = order.Products.Select(product => new
                    {
                        product.Id, product.Price, product.Weight, product.Name, product.Description, product.Category
                    })
                })
                .ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("confirms")]
    public async Task<IActionResult> GetConfirms()
    {
        int.TryParse(HttpContext.User.FindFirstValue("Id"), out var id);

        var company = await _context.Companies.FindAsync(id);
        var orders = await _context.Orders
            .Where(order => order.IsConfirm && order.Products.Any(product =>
                product.Company != null && product.Company.Name.Equals(company.Name)))
            .ToListAsync();

        // TODO: Add address

        return Ok(orders.Select(order => _mapper.Map<OrderDto>(order)).ToList());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Confirm(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = await _context.Orders.FindAsync(id);
        var couriers = await _context.Couriers.Where(courier => courier.IsFree).ToListAsync();
        var random = new Random().Next(couriers.Count);
        
        order!.Id = id;
        order.IsConfirm = true;

        couriers[random].IsFree = false;
        couriers[random].Order = order;

        _context.Couriers.Update(couriers[random]);
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<OrderDto>(order));
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
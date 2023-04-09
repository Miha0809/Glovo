using Glovo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Glovo.Controllers.Client;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Client,Moderator")]
public class OrderController : ControllerBase
{
    private readonly GlovoDbContext _context;

    public OrderController(GlovoDbContext context)
    {
        _context = context;
    }
}

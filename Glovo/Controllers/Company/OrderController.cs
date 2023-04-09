using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Glovo.Controllers.Company;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Company,Moderator")]
public class OrderController : Controller
{
    
}

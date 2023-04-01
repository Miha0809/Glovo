using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Glovo.Services;

public class RolesInDBAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
{
    private readonly GlovoDbContext _context;

    public RolesInDBAuthorizationHandler(GlovoDbContext context)
    {
        _context = context;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RolesAuthorizationRequirement requirement)
    {
        if (context.User == null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var found = false;

        if (requirement.AllowedRoles == null ||
            requirement.AllowedRoles.Any() == false)
        {
            // it means any logged in user is allowed to access the resource
            found = true;
        }
        else
        {
            var roles = requirement.AllowedRoles.ToList();
            var roleIds = await _context.Roles
                .Where(p => roles.Contains(p.Name))
                .Select(p => p.Id)
                .ToListAsync();
            var userRole = context.User.FindFirst("Role").Value;

            found = await _context.Roles
                .Where(p => roleIds.Contains(p.Id) && p.Name.Equals(userRole))
                .AnyAsync();
        }

        if (found)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
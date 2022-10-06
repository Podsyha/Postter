using System.Web.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Postter.Infrastructure.DTO;

namespace Postter.Common.Attribute;

/// <summary>
/// Кастомный аттрибут проверки ролей
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : System.Attribute, IAuthorizationFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="roles"></param>
    public CustomAuthorizeAttribute(RolesEnum roles)
    {
        _roles = roles;
    }

    public CustomAuthorizeAttribute()
    {
    }

    private readonly RolesEnum _roles;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        if (!context.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException();

        if (_roles != 0)
        {
            if (!context.HttpContext.User.Claims.Any(x => x.Value == _roles.ToString()))
                throw new UnauthorizedAccessException();
        }
    }
}
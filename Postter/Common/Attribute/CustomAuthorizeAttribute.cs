using System.Web.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Postter.Common.Helpers;

namespace Postter.Common.Attribute;

/// <summary>
/// Кастомный аттрибут проверки ролей
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : System.Attribute, IAuthorizationFilter
{
    public CustomAuthorizeAttribute(params RolesEnum[] roles)
    {
        _roles = roles;
    }

    public CustomAuthorizeAttribute()
    {
    }

    private readonly RolesEnum[] _roles;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        if (!context.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException();

        if (_roles == null)
            return;

        foreach (RolesEnum rolesEnum in _roles)
            if (_roles.Any(role => context.HttpContext.User.Claims.Any(x => x.Value == rolesEnum.ToString())))
                return;


        throw new UnauthorizedAccessException();
    }
}
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Postter.Common.Helpers;

namespace Postter.Common.Attribute;

/// <summary>
/// Кастомный аттрибут авторизации и валидации токенов
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : System.Attribute, IAuthorizationFilter, ICustomAuthorizeAttribute
{
    public CustomAuthorizeAttribute(params RolesEnum[] roles)
    {
        _roles = roles;
    }

    public CustomAuthorizeAttribute()
    {
    }

    private readonly RolesEnum[] _roles;
    private static Dictionary<Guid, string> _jwtDictionary = new();
    
    
    public void AddToken(Guid authorId, string jwt)
    {
        string token = _jwtDictionary.GetValueOrDefault(authorId);
        if (token == null)
        {
            _jwtDictionary.Add(authorId, jwt);
        }
        else
        {
            RemoveToken(authorId);
            _jwtDictionary.Add(authorId, jwt);
        }
    }
    
    public void RemoveToken(Guid authorId) => 
        _jwtDictionary.Remove(authorId);

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        if (!context.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedAccessException();

        Guid authorId = new Guid(context.HttpContext.User.Identity.GetUserId());
        bool isValidToken = _jwtDictionary.GetValueOrDefault(authorId) != null;
        if(!isValidToken)
            throw new UnauthorizedAccessException();

        if (_roles == null)
            return;

        foreach (RolesEnum rolesEnum in _roles)
            if (_roles.Any(role => context.HttpContext.User.Claims.Any(x => x.Value == rolesEnum.ToString())))
                return;


        throw new UnauthorizedAccessException();
    }
}
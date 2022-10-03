using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.UseCases.Account;

namespace Postter.Controllers.Account;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    public AccountController(IUseCaseAccount useCaseAccount)
    {
        _useCaseAccount = useCaseAccount;
    }

    private readonly IUseCaseAccount _useCaseAccount;

    
    
    [HttpGet("/token")]
    [AllowAnonymous]
    public async Task<IActionResult> Token(string username, string password)
    {
        ClaimsIdentity identity = await _useCaseAccount.GetIdentity(username, password);
        if (identity == null)
            return BadRequest("Неверная почта или пароль.");

        JwtSecurityToken token = _useCaseAccount.GetToken(username, password, identity);
        
        string encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return Ok(response);
    }

    public async Task<IActionResult> Register(string email, string password)
    {
        await _useCaseAccount.Register(email, password);

        await Token(email, password);

        return Ok();
    }

    [HttpPost("/GiveAdminRole")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GiveTheUserARole(string email, string role)
    {
        await _useCaseAccount.GiveTheUserARole(email, role);
        
        return Ok();
    }
}
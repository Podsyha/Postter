using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Postter.Common.Auth;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.Repository.Persons;
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

    
    
    [HttpPost("/token")]
    public async Task<IActionResult> Token(string username, string password)
    {
        ClaimsIdentity identity = await _useCaseAccount.GetIdentity(username, password);
        if (identity == null)
            return BadRequest("Неверная почта или пароль.");

        JwtSecurityToken token = await _useCaseAccount.GetToken(username, password, identity);
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };

        return Ok(response);
    }
    
    [HttpGet("/authorize")]
    [Authorize]
    public IActionResult GetTestAuth()
    {
        return Ok("Test");
    }
    
    [HttpGet("/admin")]
    [Authorize(Roles = "admin")]
    public IActionResult GetTest()
    {
        return Ok("Test");
    }
}
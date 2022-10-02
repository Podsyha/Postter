using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Postter.Common.Auth;
using Postter.Infrastructure.DAO;

namespace Postter.Controllers.Account;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    public AccountController()
    {
    }


    private List<Person> people = new List<Person>
    {
        new Person { Login = "admin@gmail.com", Password = "12345", Role = "admin" },
        new Person { Login = "qwerty@gmail.com", Password = "55555", Role = "user" }
    };

    [HttpPost("/token")]
    public IActionResult Token(string username, string password)
    {
        ClaimsIdentity identity = GetIdentity(username, password);
        if (identity == null)
            return BadRequest("Неверная почта или пароль.");

        DateTime now = DateTime.UtcNow;
        
        JwtSecurityToken jwt = new(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

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

    private ClaimsIdentity GetIdentity(string username, string password)
    {
        Person person = people.FirstOrDefault(person => person.Login == username && person.Password == password);

        if (person == null) return null;

        List<Claim> claims = new()
        {
            new(ClaimsIdentity.DefaultNameClaimType, person.Login),
            new(ClaimsIdentity.DefaultRoleClaimType, person.Role)
        };

        ClaimsIdentity claimsIdentity =
            new(claims, "Token", 
                ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
        
        return claimsIdentity;
    }
}
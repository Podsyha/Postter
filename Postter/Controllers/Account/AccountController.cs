﻿using System.IdentityModel.Tokens.Jwt;
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
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return Ok(response);
    }

    [HttpPost("/GiveAdminRole")]
    [Authorize(Roles = "admin")]
    public async Task GiveTheUserARole(string email, string role)
    {
        
    }

    [HttpGet("/authorize")]
    [Authorize]
    public IActionResult CheckAuth()
    {
        return Ok("Test");
    }
    
    [HttpGet("/admin")]
    [Authorize(Roles = "admin")]
    public IActionResult CheckAdmin()
    {
        return Ok("u are an admin");
    }
    
    [HttpGet("/moder")]
    [Authorize(Roles = "moder")]
    public IActionResult CheckModer()
    {
        return Ok("u are an moder");
    }
    
    [HttpGet("/user")]
    [Authorize(Roles = "user")]
    public IActionResult CheckUser()
    {
        return Ok("u are an user");
    }
}
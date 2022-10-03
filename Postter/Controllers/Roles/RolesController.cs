using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Infrastructure.DAO;
using Postter.UseCases.Roles;

namespace Postter.Controllers.Roles;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    public RoleController(IUseCaseRole useCaseRole)
    {
        _useCaseRole = useCaseRole;
    }

    private readonly IUseCaseRole _useCaseRole;


    [HttpGet("/roles")]
    [Authorize(Roles = "admin")]
    public async Task<List<Role>> GetAllRoles() => 
        await _useCaseRole.GetAllRoles();
    
    [HttpGet("/adminCheck")]
    [Authorize(Roles = "admin")]
    public IActionResult CheckAdmin()
    {
        return Ok("u are an admin");
    }
    
    [HttpGet("/moderCheck")]
    [Authorize(Roles = "moder")]
    public IActionResult CheckModer()
    {
        return Ok("u are an moder");
    }
    
    [HttpGet("/userCheck")]
    [Authorize(Roles = "user")]
    public IActionResult CheckUser()
    {
        return Ok("u are an user");
    }
    
    [HttpGet("/authorizeCheck")]
    [Authorize]
    public IActionResult CheckAuth()
    {
        return Ok("Test");
    }
}
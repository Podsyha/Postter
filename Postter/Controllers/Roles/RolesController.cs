using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postter.Common.Attribute;
using Postter.Common.Helpers.ApiResponse;
using Postter.Infrastructure.DAO;
using Postter.Infrastructure.DTO;
using Postter.UseCases.UseCaseRole;

namespace Postter.Controllers.Roles;

/// <summary>
/// Контроллер ролей
/// </summary>
[ApiController]
[Route("[controller]")]
public class RoleController : CustomController
{
    public RoleController(IUseCaseRole useCaseRole)
    {
        _useCaseRole = useCaseRole;
    }

    private readonly IUseCaseRole _useCaseRole;


    [HttpGet("/roles")]
    [CustomAuthorize(RolesEnum.Admin)]
    public async Task<List<RoleEntity>> GetAllRoles() => 
        await _useCaseRole.GetAllRoles();
    
    [HttpGet("/adminCheck")]
    [CustomAuthorize(RolesEnum.Admin)]
    public IActionResult CheckAdmin()
    {
        return Ok("u are an admin");
    }
    
    [HttpGet("/moderCheck")]
    [CustomAuthorize(RolesEnum.Moder)]
    public IActionResult CheckModer()
    {
        return Ok("u are an moder");
    }
    
    [HttpGet("/userCheck")]
    [CustomAuthorize(RolesEnum.User)]
    public IActionResult CheckUser()
    {
        return Ok("u are an user");
    }
    
    [HttpGet("/authorizeCheck")]
    [CustomAuthorize]
    public IActionResult CheckAuth()
    {
        return Ok("Test");
    }
}
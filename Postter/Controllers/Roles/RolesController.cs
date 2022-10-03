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
    public async Task<Role> GetAllRoles() => 
        await _useCaseRole.GetAllRoles();
}
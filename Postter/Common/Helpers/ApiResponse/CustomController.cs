using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Postter.Infrastructure.DTO;

namespace Postter.Common.Helpers.ApiResponse;

/// <summary>
/// Кастомный контроллер для возврата ответов в своём классе в JSON 
/// </summary>
public class CustomController : ControllerBase
{
    public override OkObjectResult Ok(object value)
    {
        ApiResponse response = new(value, null);

        return base.Ok(response);
    }

    public override BadRequestObjectResult BadRequest(object error)
    {
        ApiResponse response = new(null, error);

        return base.BadRequest(response);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public void CheckCurrentUser(Guid accountId)
    {
        Guid currentUserId = new Guid(HttpContext.User.Identity.GetUserId());

        if (currentUserId != accountId)
            throw new UnauthorizedAccessException();
    }

    /// <summary>
    /// Проверить, есть ли у пользователя данная роль
    /// </summary>
    /// <param name="role">Роль</param>
    /// <returns></returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    public bool IsCurrentRole(RolesEnum role) =>
        role != 0 && HttpContext.User.Claims.Any(x => x.Value == role.ToString());
}
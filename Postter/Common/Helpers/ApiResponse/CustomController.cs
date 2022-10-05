using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Postter.Common.Helpers.ApiResponse;

/// <summary>
/// Кастомный контроллер для возврата ответов в своём классе JSON 
/// </summary>
public class CustomController : ControllerBase
{
    public override OkObjectResult Ok(object value)
    {
        ApiResponse response = new()
        {
            Message = value, 
            Error = null
        };
        
        return base.Ok(response);
    }

    public override BadRequestObjectResult BadRequest(object error)
    {
        ApiResponse response = new()
        {
            Message = null, 
            Error = error
        };
        
        return base.BadRequest(response);
    }
    
    public void CheckCurrentUser(Guid accountId)
    {
        Guid currentUserId = new Guid(HttpContext.User.Identity.GetUserId());

        if (currentUserId != accountId)
            throw new UnauthorizedAccessException();
    }
}
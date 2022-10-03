using Microsoft.AspNetCore.Mvc;

namespace Postter.Common.Helpers.ApiResponse;

public class CustomController : ControllerBase
{
    public override OkObjectResult Ok(object value)
    {
        ApiResponse response = new()
        {
            message = value, 
            error = null
        };
        
        return base.Ok(response);
    }

    public override BadRequestObjectResult BadRequest(object error)
    {
        ApiResponse response = new()
        {
            message = null, 
            error = error
        };
        
        return base.BadRequest(response);
    }
}
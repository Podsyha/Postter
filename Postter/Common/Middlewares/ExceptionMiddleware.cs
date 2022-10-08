using System.Net;
using System.Text.Json;
using Postter.Common.Exceptions;
using Postter.Common.Helpers.ApiResponse;

namespace Postter.Common.Middlewares;

/// <summary>
/// Обработчик ошибок
/// </summary>
public sealed class ExceptionMiddleware
{
    public ExceptionMiddleware(RequestDelegate nextRequestDelegate)
    {
        _nextRequestDelegate = nextRequestDelegate;
    }

    private readonly RequestDelegate _nextRequestDelegate;
    
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _nextRequestDelegate(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                NullReferenceException x => (int)HttpStatusCode.BadRequest,
                RequestLogicException x => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException x => (int)HttpStatusCode.Unauthorized,
                DirectoryNotFoundException x => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            string result = JsonSerializer.Serialize(new ApiResponse(null, error?.Message));
            await response.WriteAsync(result);
        }
    }
}
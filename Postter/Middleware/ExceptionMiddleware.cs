using System.Net;
using System.Text.Json;
using Postter.BusinessLogic.Exceptions;

namespace Postter.Middleware;

public class ExceptionMiddleware
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
                NullReferenceException e => (int)HttpStatusCode.BadRequest,
                RequestLogicException e => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            string result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
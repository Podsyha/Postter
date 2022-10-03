using System.Net;
using System.Text.Json;
using Postter.Common.Exceptions;

namespace Postter.Common.Middlewares;

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
                _ => (int)HttpStatusCode.InternalServerError
            };

            string result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
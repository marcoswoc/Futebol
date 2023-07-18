using Futebol.Shared.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Futebol.Web.Core.Middlewares;
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            var result = JsonSerializer.Serialize(new ResponseModel { Success = false, Message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Ictx.WebApp.Api.AppStartUp.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            this._logger.LogError(e, e.Message);

            var pd = new ProblemDetails
            {
                Type = "https://demo.api.com/errors/internal-server-error",
                Title = "Server error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Server error",
            };

            pd.Extensions.Add("RequestId", context.TraceIdentifier);   

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(pd, pd.GetType(), null, contentType: "application/problem+json");
        }
    }
}
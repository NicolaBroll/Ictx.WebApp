using System;
using System.Threading.Tasks;
using Ictx.WebApp.Fwk.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ictx.WebApp.Api.Controllers;

public class AppBaseController : ControllerBase
{
    protected ActionResult FailResponse(Exception ex)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Server error",
            Detail = ex.Message
        };

        problemDetails.Extensions.Add("requestId", HttpContext.TraceIdentifier);

        switch (ex)
        {
            case BadRequestException badRequestException:
                problemDetails.Type = "https://demo.api.com/errors/bad-request";
                problemDetails.Status = StatusCodes.Status400BadRequest;

                problemDetails.Extensions.Add("errors", badRequestException.Errors);
                
                return BadRequest(problemDetails);

            case NotFoundException:
                problemDetails.Type = "https://demo.api.com/errors/not-found";
                problemDetails.Status = StatusCodes.Status404NotFound;

                return NotFound(problemDetails);

            case UnauthorizedException:
                problemDetails.Type = "https://demo.api.com/errors/unauthorized";
                problemDetails.Status = StatusCodes.Status401Unauthorized;

                return Unauthorized(problemDetails);

            case TaskCanceledException:
                problemDetails.Type = "https://demo.api.com/errors/accepted";
                problemDetails.Status = StatusCodes.Status202Accepted;

                return Accepted(problemDetails);

            default:
                problemDetails.Type = "https://demo.api.com/errors/internal-server-error";
                problemDetails.Status = StatusCodes.Status500InternalServerError;

                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}
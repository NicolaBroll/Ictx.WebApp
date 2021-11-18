using System;
using System.Threading.Tasks;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Core.Exceptions;
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

        problemDetails.Extensions.Add("RequestId", HttpContext.TraceIdentifier);

        switch (ex)
        {
            case BadRequestException:
                problemDetails.Type = "https://demo.api.com/errors/bad-request";
                problemDetails.Status = StatusCodes.Status400BadRequest;

                return BadRequest(problemDetails);

            case NotFoundException:
                problemDetails.Type = "https://demo.api.com/errors/not-found";
                problemDetails.Status = StatusCodes.Status404NotFound;

                return NotFound(problemDetails);

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

    protected async Task<UserData> GetUserData()
    {
        var claim = HttpContext.User.FindFirst("sub");

        if (claim != null)
        {
            await Task.FromResult(new UserData(int.Parse(claim.Value)));
        }

        return null;
    }
}
using System;
using System.Net;
using System.Threading.Tasks;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ictx.WebApp.Api.Controllers;

public class AppBaseController : ControllerBase
{
    protected ActionResult FailResponse(Exception ex)
    {
        if (ex is BadRequestException badRequestException)
        {  
            return BadRequest(new ErrorResponseDto(ex.Message, badRequestException.Errors));
        }

        if (ex is NotFoundException)
        {
            return NotFound(new ErrorResponseDto(ex.Message));
        }

        if (ex is TaskCanceledException)
        {
            return StatusCode(499, new ErrorResponseDto(ex.Message));
        }

        return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponseDto(ex.Message));
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
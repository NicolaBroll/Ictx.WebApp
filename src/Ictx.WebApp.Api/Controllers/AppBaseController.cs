using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ictx.WebApp.Api.Controllers
{
    public class AppBaseController : ControllerBase
    {
        private readonly IMapper _mapper;

        public AppBaseController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        protected ActionResult<R> ApiResponse<T, R>(OperationResult<T> result)
        {
            // Success.
            if (result.IsSuccess)
            {
                return Ok(_mapper.Map<R>(result.ResultData));
            }

            // Fail.
            return FailResponse(result.Exception);
        }

        protected ActionResult<T> ApiResponse<T>(OperationResult<T> result)
        {
            // Success.
            if (result.IsSuccess)
            {
                return Ok(result.ResultData);
            }

            // Fail.
            return FailResponse(result.Exception);
        }

        private ActionResult FailResponse(Exception ex)
        {
            var errorMessage = new ErrorResponseDto(ex.Message);

            if (ex is BadRequestException)
            {
                return BadRequest(errorMessage);
            }

            if (ex is NotFoundException)
            {
                return NotFound(errorMessage);
            }

            if (ex is TaskCanceledException)
            {
                return StatusCode(499, errorMessage);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, errorMessage);
        }
    }
}

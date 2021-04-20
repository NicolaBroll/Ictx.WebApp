using System.Net;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.BO.Base;
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

        protected ActionResult<R> ApiResponse<T, R>(BOResult<T> dipendenteResult)
        {
            // Success.
            if (dipendenteResult.IsSuccess)
            {
                return Ok(_mapper.Map<R>(dipendenteResult.ResultData));
            }

            var errorMessage = new ErrorResponseDto(dipendenteResult.Exception.Message);

            // Fail.
            if (dipendenteResult.Exception is BadRequestException) 
            {
                return BadRequest(errorMessage);
            }

            if (dipendenteResult.Exception is NotFoundException) 
            {
                return NotFound(errorMessage);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, errorMessage);
        }
    }
}

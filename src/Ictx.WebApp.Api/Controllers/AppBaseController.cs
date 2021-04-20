using System.Net;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using LanguageExt.Common;
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

        protected ActionResult<R> ApiResponse<T, R>(Result<T> dipendenteResult)
        {
            return dipendenteResult.Match<ActionResult<R>>(
                (succ) => Ok(_mapper.Map<R>(succ)),
                (fail) => {

                    var errorMessage = new ErrorResponseDto(fail.Message);

                    if (fail is BadRequestException)
                        return BadRequest(errorMessage);

                    if (fail is NotFoundException)
                        return NotFound(errorMessage);

                    return StatusCode((int)HttpStatusCode.InternalServerError, errorMessage);
                });
        }
    }
}

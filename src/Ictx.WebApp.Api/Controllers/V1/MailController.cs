using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Api.Controllers.V1
{
    [ApiController] 
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MailController : AppBaseController
    {
        private readonly MailBO _mailBO;

        public MailController(IMapper mapper, MailBO mailBO) : base(mapper)
        {
            this._mailBO = mailBO;
        }

        /// <summary>
        /// Crea una mail.
        /// </summary>
        /// <param name="model">Dto rappresentante la mail.</param>
        /// <returns></returns> 
        [HttpPost("")]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DipendenteEmailTemplate>> Post([FromBody] DipendenteEmailTemplate model)
        {
            var result = await this._mailBO.InsertAsync(model);

            return ApiResponse(result);
        }
    }
}

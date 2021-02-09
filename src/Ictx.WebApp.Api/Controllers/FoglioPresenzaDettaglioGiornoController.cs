using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Infrastructure.Services.Interface;

namespace Ictx.WebApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoglioPresenzaDettaglioGiornoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFoglioPresenzaDettaglioGiornoService _foglioPresenzaDettaglioGiornoService;

        public FoglioPresenzaDettaglioGiornoController(IMapper mapper, IFoglioPresenzaDettaglioGiornoService foglioPresenzaDettaglioGiornoService)
        {
            this._mapper = mapper;
            this._foglioPresenzaDettaglioGiornoService = foglioPresenzaDettaglioGiornoService;
        }

        // DELETE: api/FoglioPresenzaDettaglioGiorno
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await this._foglioPresenzaDettaglioGiornoService.Delete(id);
            return Ok();
        }
    }
}

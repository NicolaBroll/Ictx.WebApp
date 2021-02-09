using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Shared.QueryParameters;
using static Ictx.WebApp.Api.Dtos.FoglioPresenzaDtos;

namespace Ictx.WebApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoglioPresenzaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFoglioPresenzaService _foglioPresenzaService;

        public FoglioPresenzaController(IMapper mapper, IFoglioPresenzaService foglioPresenzaService)
        {
            this._mapper = mapper;
            this._foglioPresenzaService = foglioPresenzaService;
        }

        // GET: api/FoglioPresenza
        [HttpGet]
        public async Task<ActionResult<FoglioPresenzaDto>> Get([FromQuery] FoglioPresenzaQueryParameter parameters)
        {
            if (parameters.DipendenteId == 0 || parameters.Anno == 0 || parameters.Mese == 0)
                return BadRequest();

            var fdp = await _foglioPresenzaService.Get(parameters.DipendenteId, parameters.Anno, parameters.Mese);
            var res = _mapper.Map<FoglioPresenzaDto>(fdp);

            return Ok(res);
        }
    }
}

using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Infrastructure.Services.Interface;
using Ictx.WebApp.Shared.Dtos;
using Ictx.WebApp.Shared.QueryParameters;

namespace Ictx.WebApp.Server.Controllers
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
        public async Task<FoglioPresenzaDto> Get([FromQuery] FoglioPresenzaQueryParameter parameters)
        {
            var fdp = await _foglioPresenzaService.Get(parameters.DipendenteId, parameters.Anno, parameters.Mese);
            var res = _mapper.Map<FoglioPresenzaDto>(fdp);

            return res;
        }
    }
}

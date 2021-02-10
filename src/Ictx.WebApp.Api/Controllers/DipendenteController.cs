using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Infrastructure.Services.Interface;
using static Ictx.WebApp.Core.Models.PaginationModel;
using static Ictx.WebApp.Api.Dtos.DipendenteDtos;
using static Ictx.WebApp.Api.Models.DipendenteModel;

namespace Ictx.WebApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DipendenteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDipendenteService _dipendenteService;

        public DipendenteController(IMapper mapper, IDipendenteService dipendenteService)
        {
            this._mapper = mapper;
            this._dipendenteService = dipendenteService;
        }

        // GET: api/Dipendente
        [HttpGet]
        public async Task<PageResult<DipendenteDto>> Get([FromQuery] DipendenteQueryParameters dipendenteQueryParameters)
        {
            var list = await _dipendenteService.GetListAsync(dipendenteQueryParameters, dipendenteQueryParameters.DittaId);
            var res = _mapper.Map<PageResult<DipendenteDto>>(list);

            return res;
        }

        // GET: api/Notifications/id
        [HttpGet("{id}")]
        public async Task<ActionResult<DipendenteDto>> GetById(int id)
        {
            var dipendente = await this._dipendenteService.GetByIdAsync(id);

            if (dipendente == null)
                return NotFound();

            var res = _mapper.Map<DipendenteDto>(dipendente);

            return Ok(res);
        }

        // DELETE: api/Dipendente/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var objDb = await this._dipendenteService.GetByIdAsync(id);

            // Verifico se esiste.
            if (objDb == null)
                return NotFound();

            if (await this._dipendenteService.DeleteAsync(id))
                return Ok();
            else
                return StatusCode(500);
        }

        // POST: api/Dipendente
        [HttpPost]
        public async Task<ActionResult<DipendenteDto>> Post([FromBody] DipendenteDto dipendenteDto)
        {
            var objToInsert = _mapper.Map<Dipendente>(dipendenteDto);

            var objDb = await this._dipendenteService.InsertAsync(objToInsert);

            var res = _mapper.Map<DipendenteDto>(objDb);

            return Ok(res);
        }

        // PUT: api/Dipendente/id
        [HttpPut("{id}")]
        public async Task<ActionResult<DipendenteDto>> Put(int id, [FromBody] DipendenteDto notification)
        {
            var objDb = await this._dipendenteService.GetByIdAsync(id);

            // Verifico se esiste.
            if (objDb == null)
                return NotFound();

            var objToUpdate = _mapper.Map<Dipendente>(notification);

            objDb = await this._dipendenteService.SaveAsync(id, objToUpdate);
 
            return Ok(_mapper.Map<DipendenteDto>(objDb));  
        }
    }
}

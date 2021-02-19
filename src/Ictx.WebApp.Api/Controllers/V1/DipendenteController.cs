using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Infrastructure.Services;
using static Ictx.WebApp.Core.Models.PaginationModel;
using static Ictx.WebApp.Api.Dtos.DipendenteDtos;
using static Ictx.WebApp.Api.Models.DipendenteModel;
using static Ictx.WebApp.Core.Models.DipendenteModel;
using static Ictx.WebApp.Api.Controllers.V1.ApiRoutes;

namespace Ictx.WebApp.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    public class DipendenteController : ControllerBase
    {
        private readonly IMapper            _mapper;
        private readonly DipendenteService _dipendenteService;

        public DipendenteController(IMapper mapper, DipendenteService dipendenteService)
        {
            this._mapper = mapper;
            this._dipendenteService = dipendenteService;
        }

        [HttpGet(DipendenteRoute.Get)]
        public async Task<PageResult<DipendenteDto>> Get([FromQuery] DipendenteQueryParameters dipendenteQueryParameters)
        {
            var filterModel = _mapper.Map<DipendenteListFilter>(dipendenteQueryParameters);            

            var list = await _dipendenteService.GetListAsync(filterModel);
            var res = _mapper.Map<PageResult<DipendenteDto>>(list);

            return res;
        }

        [HttpGet(DipendenteRoute.GetById)]
        public async Task<ActionResult<DipendenteDto>> GetById(int id)
        {
            try
            {
                var dipendente = await this._dipendenteService.GetByIdAsync(id);
                return Ok(_mapper.Map<DipendenteDto>(dipendente));
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorResponse("Errore durante la lettura del dato.", ex.Message));
            }
        }

        [HttpDelete(DipendenteRoute.Delete)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this._dipendenteService.DeleteAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(new ErrorResponse("Errore durante l'eliminazione del dato.", ex.Message));
            }
        }

        [HttpPost(DipendenteRoute.Post)]
        public async Task<ActionResult<DipendenteDto>> Post([FromBody] DipendenteDto model)
        {
            try
            {
                var objToInsert = _mapper.Map<Dipendente>(model);
                var objDb = await this._dipendenteService.InsertAsync(objToInsert);

                return Ok(_mapper.Map<DipendenteDto>(objDb));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse("Errore durante l'inserimento del dato.", ex.Message));
            }
        }

        [HttpPut(DipendenteRoute.Put)]
        public async Task<ActionResult<DipendenteDto>> Put(int id, [FromBody] DipendenteDto model)
        {
            try
            {
                var objToUpdate = _mapper.Map<Dipendente>(model);
                var objDb = await this._dipendenteService.SaveAsync(id, objToUpdate);

                return Ok(_mapper.Map<DipendenteDto>(objDb));
            }
            catch (DipendenteNotFoundException ex)
            {
                return NotFound(new ErrorResponse("Errore durante la modifica del dato.", ex.Message));
            }
            catch (DittaNotFoundException ex)
            {
                return BadRequest(new ErrorResponse("Errore durante la modifica del dato.", ex.Message));
            }
        }
    }
}

using System.Net;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Api.Common;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Exceptions.Dipendente;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Infrastructure.Services.Interfaces;
using static Ictx.WebApp.Api.Controllers.V1.ApiRoutesV1;

namespace Ictx.WebApp.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class DipendenteController : ControllerBase
    {
        private readonly IMapper            _mapper;
        private readonly IDipendenteService  _dipendenteService;

        public DipendenteController(IMapper mapper, IDipendenteService dipendenteService)
        {
            this._mapper            = mapper;
            this._dipendenteService = dipendenteService;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata per una determinata ditta.
        /// </summary>
        /// <param name="dipendenteQueryParameters">Filtro per ditta ed elementi di paginazione.</param>
        /// <response code = "200">Ritorna la lista paginata di dipendenti.</response>
        /// <returns></returns>
        [HttpGet(DipendenteRoute.Get)]
        [ProducesResponseType(typeof(PageResult<DipendenteDto>), (int)HttpStatusCode.OK)]
        public async Task<PageResult<DipendenteDto>> Get([FromQuery] DipendenteListFilter dipendenteQueryParameters)
        {
            var list = await _dipendenteService.GetListAsync(dipendenteQueryParameters);
            var res = _mapper.Map<PageResult<DipendenteDto>>(list);

            return res;
        }

        /// <summary>
        /// Ritorna un singolo dipendente.
        /// </summary>
        /// <param name="id">Identificativo dipendente.</param>
        /// <response code = "200">Ritorna un dipendente.</response>
        /// <response code = "404">Ritorna un ErrorResponse in quanto nel database non è presente un dipendente con
        /// l'identificativo richesto.</response>
        /// <returns></returns>
        [HttpGet(DipendenteRoute.GetById)]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DipendenteDto>> GetById(int id)
        {
            var dipendenteResult = await this._dipendenteService.GetByIdAsync(id);

            return dipendenteResult.Match<ActionResult<DipendenteDto>>(
                (succ) => Ok(_mapper.Map<DipendenteDto>(succ)),
                (fail) => NotFound(new ErrorResponse("Errore durante la lettura del dato.", fail.Message))
                );
        }

        /// <summary>
        /// Elimina un singolo dipendente.
        /// </summary>
        /// <param name="id">Identificativo dipendente.</param>
        /// <response code = "200">Dipendente eliminato con successo.</response>
        /// <response code = "404">Ritorna un ErrorResponse in quanto nel database non è presente un dipendente con
        /// l'identificativo richesto.</response>
        /// <returns></returns>
        [HttpDelete(DipendenteRoute.Delete)]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await this._dipendenteService.DeleteAsync(id);

            return result.Match<ActionResult>(
                (succ) => Ok(),
                (fail) => NotFound(new ErrorResponse("Errore durante l'eliminazione del dato.", fail.Message))
                );
        }

        /// <summary>
        /// Crea un singolo dipendente.
        /// </summary>
        /// <param name="model">Dto rappresentante il dipendente.</param>
        /// <response code = "200">Dipendente creato correttamente, ritorna l'oggetto creato.</response>
        /// <response code = "400">Errore validazione dto.</response>
        /// <returns></returns> 
        [HttpPost(DipendenteRoute.Post)]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DipendenteDto>> Post([FromBody] DipendenteDto model)
        {
            var objToInsert = _mapper.Map<Dipendente>(model);
            var result = await this._dipendenteService.InsertAsync(objToInsert);

            return result.Match<ActionResult>(
                (succ) => Ok(_mapper.Map<DipendenteDto>(succ)),
                (fail) => {
                    var errorResponse = new ErrorResponse("Errore durante l'inserimento del dato.", fail.Message);

                    if (fail is BadRequestException)
                        return BadRequest(errorResponse);

                    return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
                });
        }

        /// <summary>
        /// Modifica un singolo dipendente.
        /// </summary>
        /// <param name="id">Identificativo dipendente.</param>
        /// <param name="model">Dto rappresentante il dipendente.</param>
        /// <response code = "200">Dipendente modificato correttamente, ritorna l'oggetto modificato.</response>
        /// <response code = "400">Errore validazione dto.</response>
        /// <response code = "404">Ritorna un ErrorResponse in quanto nel database non è presente un dipendente
        /// con l'identificativo richesto.</response>
        /// <returns></returns>
        [HttpPut(DipendenteRoute.Put)]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DipendenteDto>> Put(int id, [FromBody] DipendenteDto model)
        {
            var objToUpdate = _mapper.Map<Dipendente>(model);
            var result = await this._dipendenteService.SaveAsync(id, objToUpdate);

            return result.Match<ActionResult>(
                (succ) => Ok(_mapper.Map<DipendenteDto>(succ)),
                (fail) => {
                    var errorResponse = new ErrorResponse("Errore durante l'inserimento del dato.", fail.Message);

                    if (fail is NotFoundException)
                        return NotFound(errorResponse);

                    if (fail is BadRequestException)
                        return BadRequest(errorResponse);

                    return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
                });
        }
    }
}

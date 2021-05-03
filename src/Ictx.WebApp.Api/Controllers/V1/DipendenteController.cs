using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Interfaces;
using Ictx.WebApp.Core.Models;
using static Ictx.WebApp.Api.Controllers.V1.ApiRoutesV1;
using Ictx.WebApp.Api.Common;

namespace Ictx.WebApp.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class DipendenteController : AppBaseController
    {
        private readonly IMapper            _mapper;
        private readonly SessionData        _sessionData;
        private readonly IDipendenteService _dipendenteService;

        public DipendenteController(IMapper mapper, SessionData sessionData, IDipendenteService dipendenteService): base(mapper)
        {
            this._mapper            = mapper;
            this._sessionData       = sessionData;
            this._dipendenteService = dipendenteService;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata per una determinata ditta.
        /// </summary>
        /// <param name="dipendenteQueryParameters">Filtro per ditta ed elementi di paginazione.</param>
        /// <response code = "200">Ritorna la lista paginata di dipendenti.</response>
        /// <returns></returns>
        [HttpGet(DipendenteRoute.Get)]
        [ProducesResponseType(typeof(PageResultDto<DipendenteDto>), (int)HttpStatusCode.OK)]
        public async Task<PageResultDto<DipendenteDto>> Get([FromQuery] DipendenteListFilter dipendenteQueryParameters)
        {
            var list = await _dipendenteService.ReadManyAsync(dipendenteQueryParameters);
            var res = _mapper.Map<PageResultDto<DipendenteDto>>(list);

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
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DipendenteDto>> GetById(int id)
        {
            var result = await this._dipendenteService.ReadAsync(id);
            return ApiResponse<Dipendente, DipendenteDto>(result);
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
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await this._dipendenteService.DeleteAsync(id);
            return ApiResponse<bool, bool>(result);
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
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DipendenteDto>> Post([FromBody] DipendenteDto model)
        {
            var objToInsert = _mapper.Map<Dipendente>(model);
            var result = await this._dipendenteService.InsertAsync(objToInsert);

            return ApiResponse<Dipendente, DipendenteDto>(result);
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
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DipendenteDto>> Put(int id, [FromBody] DipendenteDto model)
        {
            var objToUpdate = _mapper.Map<Dipendente>(model);
            var result = await this._dipendenteService.SaveAsync(id, objToUpdate);

            return ApiResponse<Dipendente, DipendenteDto>(result);
        }
    }
}

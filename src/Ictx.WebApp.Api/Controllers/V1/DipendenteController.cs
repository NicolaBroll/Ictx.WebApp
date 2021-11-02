﻿using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Application.Models;

namespace Ictx.WebApp.Api.Controllers.V1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DipendenteController : AppBaseController
    {
        private readonly IMapper _mapper;
        private readonly DipendenteBO _dipendenteBO;

        public DipendenteController(IMapper mapper, DipendenteBO dipendenteBO) : base(mapper)
        {
            this._mapper = mapper;
            this._dipendenteBO = dipendenteBO;
        }

        /// <summary>
        /// Ritorna una lista di dipendenti paginata.
        /// </summary>
        /// <param name="paginationModel">Filtro per elementi di paginazione.</param>
        /// <param name="cancellationToken"></param>
        /// <response code = "200">Ritorna la lista paginata di dipendenti.</response>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(PageResultDto<DipendenteDto>), (int)HttpStatusCode.OK)]
        [Authorize("Api1_r")]
        public async Task<PageResultDto<DipendenteDto>> Get([FromQuery] PaginationModel paginationModel, CancellationToken cancellationToken)
        {
            var list = await _dipendenteBO.ReadManyPaginatedAsync(
                paginationModel, 
                cancellationToken);

            var res = _mapper.Map<PageResultDto<DipendenteDto>>(list);

            return res;
        }

        /// <summary>
        /// Ritorna un singolo dipendente.
        /// </summary>
        /// <param name="id">Identificativo dipendente.</param>
        /// <param name="cancellationToken"></param>
        /// <response code = "200">Ritorna un dipendente.</response>
        /// <response code = "404">Ritorna un ErrorResponse in quanto nel database non è presente un dipendente con
        /// l'identificativo richesto.</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DipendenteDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await this._dipendenteBO.ReadAsync(id, cancellationToken);
            return ApiResponse<Dipendente, DipendenteDto>(result);
        }

        /// <summary>
        /// Elimina un singolo dipendente.
        /// </summary>
        /// <param name="id">Identificativo dipendente.</param>
        /// <param name="cancellationToken"></param>
        /// <response code = "200">Dipendente eliminato con successo.</response>
        /// <response code = "404">Ritorna un ErrorResponse in quanto nel database non è presente un dipendente con
        /// l'identificativo richesto.</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await this._dipendenteBO.DeleteAsync(id, cancellationToken: cancellationToken);
            return ApiResponse<bool, bool>(result);
        }

        /// <summary>
        /// Crea un singolo dipendente.
        /// </summary>
        /// <param name="model">Dto rappresentante il dipendente.</param>
        /// <param name="cancellationToken"></param>
        /// <response code = "200">Dipendente creato correttamente, ritorna l'oggetto creato.</response>
        /// <response code = "400">Errore validazione dto.</response>
        /// <returns></returns> 
        [HttpPost("")]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DipendenteDto>> Post([FromBody] DipendenteDto model, CancellationToken cancellationToken)
        {
            var objToInsert = _mapper.Map<Dipendente>(model);
            var result = await this._dipendenteBO.InsertAsync(objToInsert, cancellationToken: cancellationToken);

            return ApiResponse<Dipendente, DipendenteDto>(result);
        }

        /// <summary>
        /// Modifica un singolo dipendente.
        /// </summary>
        /// <param name="id">Identificativo dipendente.</param>
        /// <param name="model">Dto rappresentante il dipendente.</param>
        /// <param name="cancellationToken"></param>
        /// <response code = "200">Dipendente modificato correttamente, ritorna l'oggetto modificato.</response>
        /// <response code = "400">Errore validazione.</response>
        /// <response code = "404">Ritorna un ErrorResponse in quanto nel database non è presente un dipendente
        /// con l'identificativo richesto.</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DipendenteDto>> Put(int id, [FromBody] DipendenteDto model, CancellationToken cancellationToken)
        {
            var objToUpdate = _mapper.Map<Dipendente>(model);
            var result = await this._dipendenteBO.SaveAsync(id, objToUpdate, cancellationToken: cancellationToken);

            return ApiResponse<Dipendente, DipendenteDto>(result);
        }
    }
}

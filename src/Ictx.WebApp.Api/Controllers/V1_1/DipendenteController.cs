using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Application.Models;

namespace Ictx.WebApp.Api.Controllers.V1_1;

[ApiController]
[ApiVersion("1.1")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DipendenteController : AppBaseController
{
    private readonly IMapper _mapper;
    private readonly DipendenteBO _dipendenteBO;

    public DipendenteController(IMapper mapper, DipendenteBO dipendenteBO)
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
    public async Task<ActionResult<PageResultDto<DipendenteDto>>> Get([FromQuery] PaginationModel paginationModel, CancellationToken cancellationToken)
    {
        var list = await _dipendenteBO.ReadManyPaginatedAsync(
            paginationModel, 
            cancellationToken);

        return Ok(_mapper.Map<PageResultDto<DipendenteDto>>(list));
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
    [HttpGet("{id}", Name= "GetDipendenteById")]
    [ProducesResponseType(typeof(DipendenteDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponseDto), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<DipendenteDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await this._dipendenteBO.ReadAsync(id, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(_mapper.Map<DipendenteDto>(result.ResultData));
        }

        return FailResponse(result.Exception);
    }        
}
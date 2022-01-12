using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Fwk.BO.Base;
using Ictx.WebApp.Fwk.Models;
using AutoMapper;

namespace Ictx.WebApp.Api.Controllers;

public class ReadOnlyController<TDto, TEntity, TKey, TParameters, ReadOnlyBo> : AppBaseController
    where TDto: class
    where TEntity : class
    where TParameters : PaginationModel
    where ReadOnlyBo : PersistableBO<TEntity, TKey, TParameters>
{
    protected readonly IMapper                                      _mapper;
    protected readonly PersistableBO<TEntity, TKey, TParameters>    _bo;

    public ReadOnlyController(IMapper mapper, PersistableBO<TEntity, TKey, TParameters> bo)
    {
        this._mapper    = mapper;
        this._bo        = bo;
    }

    [HttpGet("")]
    public async Task<ActionResult<PageResult<TDto>>> Get([FromQuery] TParameters parameters, CancellationToken cancellationToken)
    {
        var list = await this._bo.ReadManyPaginatedAsync(
            parameters,
            cancellationToken);

        return Ok(_mapper.Map<PageResult<TDto>>(list));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> GetById(TKey id, CancellationToken cancellationToken)
    {
        var (Data, Exception) = await this._bo.ReadAsync(id, cancellationToken);

        if (Exception is null)
        {
            return Ok(_mapper.Map<TDto>(Data));
        }

        return FailResponse(Exception);
    }
}
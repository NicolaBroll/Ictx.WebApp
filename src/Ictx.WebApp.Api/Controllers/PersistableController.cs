using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Fwk.BO.Base;
using Ictx.WebApp.Fwk.Models;
using AutoMapper;
using Ictx.WebApp.Fwk.Entities.Base;

namespace Ictx.WebApp.Api.Controllers;

public class PersistableController<TDto, TEntity, TKey, TParameters, PersistableBo> : ReadOnlyController<TDto, TEntity, TKey, TParameters, PersistableBo>
    where TDto: class
    where TEntity : BaseEntity<TKey>
    where TParameters : PaginationModel
    where PersistableBo : PersistableBO<TEntity, TKey, TParameters>
{
    public PersistableController(IMapper mapper, PersistableBO<TEntity, TKey, TParameters> bo) : base(mapper, bo)
    { }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(TKey id, CancellationToken cancellationToken)
    {
        var result = await this._bo.DeleteAsync(id, cancellationToken: cancellationToken);

        if (result.Exception is null)
        {
            return true;
        }

        return FailResponse(result.Exception);
    }

    [HttpPost("")]
    public async Task<ActionResult<TDto>> Post([FromBody] TDto model, CancellationToken cancellationToken)
    {
        var objToInsert = _mapper.Map<TEntity>(model);
        var result = await this._bo.InsertAsync(objToInsert, cancellationToken: cancellationToken);

        if (result.Exception is null)
        {
            return Ok( _mapper.Map<TDto>(result.Data));
        }

        return FailResponse(result.Exception);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TDto>> Put(TKey id, [FromBody] TDto model, CancellationToken cancellationToken)
    {
        var objToUpdate = _mapper.Map<TEntity>(model);
        var result = await this._bo.SaveAsync(id, objToUpdate, cancellationToken: cancellationToken);

        if (result.Exception is null)
        {
            return Ok(_mapper.Map<TDto>(result.Data));
        }

        return FailResponse(result.Exception);
    }
}
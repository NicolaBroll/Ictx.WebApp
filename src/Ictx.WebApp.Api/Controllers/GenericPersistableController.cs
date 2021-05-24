using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Application.Models;
using Ictx.WebApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Ictx.WebApp.Api.Controllers
{
    public class GenericPersistableController<TEntity, TEntityDto, Parameters, Key> : AppBaseController where Parameters : PaginationModel 
    {
        private readonly IMapper _mapper;
        private readonly BaseBO<TEntity, Key, Parameters> _baseBO;

        public GenericPersistableController(IMapper mapper, BaseBO<TEntity, Key, Parameters> baseBO) : base(mapper)
        {
            this._mapper = mapper;
            this._baseBO = baseBO;
        }

        [HttpGet]
        public async virtual Task<PageResultDto<TEntityDto>> Get([FromQuery] Parameters paginationModel, CancellationToken cancellationToken = default)
        {
            var list = await _baseBO.ReadManyAsync(paginationModel, cancellationToken);
            var res = _mapper.Map<PageResultDto<TEntityDto>>(list);

            return res;
        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<TEntityDto>> GetById(Key id, CancellationToken cancellationToken = default)
        {
            var result = await this._baseBO.ReadAsync(id, cancellationToken);
            return ApiResponse<TEntity, TEntityDto>(result);
        }

        [HttpDelete("{id}")]
        public async virtual Task<ActionResult<bool>> Delete(Key id, CancellationToken cancellationToken = default)
        {
            var result = await this._baseBO.DeleteAsync(id, cancellationToken);
            return ApiResponse(result);
        }

        [HttpPost]
        public async virtual Task<ActionResult<TEntityDto>> Post([FromBody] TEntityDto model, CancellationToken cancellationToken = default)
        {
            var objToInsert = _mapper.Map<TEntity>(model);
            var result = await this._baseBO.InsertAsync(objToInsert, cancellationToken);

            return ApiResponse<TEntity, TEntityDto>(result);
        }

        [HttpPut("{id}")]
        public async virtual Task<ActionResult<TEntityDto>> Put(Key id, [FromBody] TEntityDto model, CancellationToken cancellationToken = default)
        {
            var objToUpdate = _mapper.Map<TEntity>(model);
            var result = await this._baseBO.SaveAsync(id, objToUpdate, cancellationToken);

            return ApiResponse<TEntity, TEntityDto>(result);
        }
    }
}


using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<PageResultDto<TEntityDto>> Get([FromQuery] Parameters paginationModel)
        {
            var list = await _baseBO.ReadManyAsync(paginationModel);
            var res = _mapper.Map<PageResultDto<TEntityDto>>(list);

            return res;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntityDto>> GetById(Key id)
        {
            var result = await this._baseBO.ReadAsync(id);
            return ApiResponse<TEntity, TEntityDto>(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Key id)
        {
            var result = await this._baseBO.DeleteAsync(id);
            return ApiResponse<bool, bool>(result);
        }

        [HttpPost]
        public async Task<ActionResult<TEntityDto>> Post([FromBody] TEntityDto model)
        {
            var objToInsert = _mapper.Map<TEntity>(model);
            var result = await this._baseBO.InsertAsync(objToInsert);

            return ApiResponse<TEntity, TEntityDto>(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TEntityDto>> Put(Key id, [FromBody] TEntityDto model)
        {
            var objToUpdate = _mapper.Map<TEntity>(model);
            var result = await this._baseBO.SaveAsync(id, objToUpdate);

            return ApiResponse<TEntity, TEntityDto>(result);
        }
    }
}


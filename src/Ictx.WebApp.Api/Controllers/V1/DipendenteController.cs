using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Application.BO;

namespace Ictx.WebApp.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DipendenteController : GenericPersistableController<Dipendente, DipendenteDto, PaginationModel, int>
    {
        public DipendenteController(IMapper mapper, DipendenteBO dipendenteBO) : base(mapper, dipendenteBO)
        { }
    }
}

using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;
using Ictx.WebApp.Application.BO;
using Ictx.WebApp.Application.Models;

namespace Ictx.WebApp.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DipendenteController : GenericPersistableController<Dipendente, DipendenteDto, PaginationModel, int>
    {
        public DipendenteController(IMapper mapper, DipendenteBO dipendenteBO) : base(mapper, dipendenteBO)
        { }
    }
}

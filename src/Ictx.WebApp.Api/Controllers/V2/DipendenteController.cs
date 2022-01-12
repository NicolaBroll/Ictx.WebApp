using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ictx.WebApp.Core.Domain.DipendenteDomain;
using Ictx.WebApp.Api.Models;

namespace Ictx.WebApp.Api.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DipendenteController : PersistableController<DipendenteDto, Dipendente, int, DipendenteFilter, DipendenteBO>
{
    public DipendenteController(IMapper mapper, DipendenteBO dipendenteBO): base(mapper, dipendenteBO)
    { }
}
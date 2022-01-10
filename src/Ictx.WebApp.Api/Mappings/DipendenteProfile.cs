using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Domain.Dipendente;
using Ictx.WebApp.Fwk.Models;

namespace Ictx.WebApp.Api.Mappings;

public class DipendenteProfile : Profile
{
	public DipendenteProfile()
	{
		// Pagination.
		CreateMap<PageResult<Dipendente>, PageResultDto<DipendenteDto>>();

		CreateMap<Dipendente, DipendenteDto>().ForMember(x => x.DataNascita, opt => opt.MapFrom(src => src.DataNascita.ToShortDateString()));

		CreateMap<DipendenteDto, Dipendente>();
	}
}
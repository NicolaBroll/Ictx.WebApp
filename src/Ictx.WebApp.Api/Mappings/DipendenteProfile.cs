using AutoMapper;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Core.Models;

namespace Ictx.WebApp.Api.Mappings
{
    public class DipendenteProfile : Profile
	{
		public DipendenteProfile()
		{
			CreateMap<PageResult<Dipendente>, PageResult<DipendenteDto>>();
			CreateMap<Dipendente, DipendenteDto>();
			CreateMap<DipendenteDto, Dipendente>();
		}
	}
}

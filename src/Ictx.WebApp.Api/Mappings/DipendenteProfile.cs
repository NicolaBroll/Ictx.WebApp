using AutoMapper;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Shared.Dtos;

namespace Ictx.WebApp.Api.Mappings
{
    public class DipendenteProfile : Profile
	{
		public DipendenteProfile()
		{
			CreateMap<Dipendente, DipendenteDto>();
			CreateMap<DipendenteDto, Dipendente>();
		}
	}
}

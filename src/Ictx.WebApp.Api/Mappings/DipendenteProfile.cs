using AutoMapper;
using Ictx.WebApp.Core.Entities;
using static Ictx.WebApp.Core.Models.PaginationModel;
using static Ictx.WebApp.Api.Dtos.DipendenteDtos;
using static Ictx.WebApp.Api.Models.DipendenteModel;
using static Ictx.WebApp.Core.Models.DipendenteModel;

namespace Ictx.WebApp.Api.Mappings
{
    public class DipendenteProfile : Profile
	{
		public DipendenteProfile()
		{
			CreateMap<PageResult<Dipendente>, PageResult<DipendenteDto>>();
			CreateMap<Dipendente, DipendenteDto>();
			CreateMap<DipendenteDto, Dipendente>();
			CreateMap<DipendenteQueryParameters, DipendenteListFilter>();
		}
	}
}

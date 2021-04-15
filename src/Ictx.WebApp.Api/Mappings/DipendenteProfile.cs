using AutoMapper;
using Ictx.Framework.Models;
using Ictx.WebApp.Api.Models;
using Ictx.WebApp.Core.Entities;

namespace Ictx.WebApp.Api.Mappings
{
    public class DipendenteProfile : Profile
	{
		public DipendenteProfile()
		{
			// Pagination.
			CreateMap<PageResult<Dipendente>, PageResultDto<DipendenteDto>>();

			CreateMap<Dipendente, DipendenteDto>();
			CreateMap<DipendenteDto, Dipendente>();
		}
	}
}

using AutoMapper;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Shared.Dtos;

namespace Ictx.WebApp.Api.Mappings
{
    public class FoglioPresenzaProfile : Profile
	{
		public FoglioPresenzaProfile()
		{
			CreateMap<FoglioPresenza, FoglioPresenzaDto>();
			CreateMap<FoglioPresenzaGiornoDettaglio, FoglioPresenzaGiornoDettaglioDto>();
			CreateMap<FoglioPresenzaGiorno, FoglioPresenzaGiornoDto>();
			CreateMap<FoglioPresenzaVpa, FoglioPresenzaVpaDto>();
		}
	}
}

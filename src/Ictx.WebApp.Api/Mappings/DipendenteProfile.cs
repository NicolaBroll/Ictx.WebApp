﻿using AutoMapper;
using Ictx.WebApp.Core.Entities;
using Ictx.WebApp.Shared.Dtos;
using static Ictx.WebApp.Core.Models.PaginationModel;

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

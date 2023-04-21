using System;
using AutoMapper;
using ViewModels;
using Dto;

namespace TheWebApplication
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<LocationDto, LocationViewModel>().ReverseMap();
			CreateMap<OriginDto, OriginViewModel>().ReverseMap();
			CreateMap<EpisodeDto, EpisodeViewModel>()
				.ForMember(dest => dest.EpisodeUrl, opt => opt.MapFrom(s => s.EpisodeUrl))
				.ReverseMap();

			CreateMap<CharacteristicDto, CharacteristicViewModel>()
				.ForMember(dest => dest.Location, opt => opt.MapFrom(s => s.Location))
				.ForMember(dest => dest.Origin, opt => opt.MapFrom(s => s.Origin))
				.ForMember(dest => dest.Episode, opt => opt.MapFrom(s => s.Episode))
				.ReverseMap();
		}
	}
}


using System;
using AutoMapper;
using ViewModels;
using Domain.Entities;
using Dto;

namespace Services
{
	public class EntitiesToDtoMappingProfile : Profile
    {
        public EntitiesToDtoMappingProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Origin, OriginDto>().ReverseMap();
            CreateMap<Episode, EpisodeDto>().ReverseMap();
            CreateMap<TypeItem, TypeItemDto>().ReverseMap();
            CreateMap<GenderItem, GenderItemDto>().ReverseMap();
            CreateMap<StatusItem, StatusItemDto>().ReverseMap();
            CreateMap<SpeciesItem, SpeciesItemDto>().ReverseMap();

            CreateMap<Characteristic, CharacteristicDto>()
                .ForMember(dest => dest.TypeItem, opt => opt.MapFrom(src => src.TypeItem))
                .ForMember(dest => dest.GenderItem, opt => opt.MapFrom(src => src.GenderItem))
                .ForMember(dest => dest.SpeciesItem, opt => opt.MapFrom(src => src.SpeciesItem))
                .ForMember(dest => dest.StatusItem, opt => opt.MapFrom(src => src.StatusItem))
                .ForMember(dest => dest.LocationItem, opt => opt.MapFrom(s => s.Location))
                .ForMember(dest => dest.OriginItem, opt => opt.MapFrom(s => s.Origin))
                .ForMember(dest => dest.Episode, opt => opt.MapFrom(s => s.Episodes.ToList()))
                .ReverseMap();
        }
    }
}


using System;
using AutoMapper;
using ViewModels;
using Domain.Entities;
using Dto;

namespace Services
{
	public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Origin, OriginDto>().ReverseMap();
            CreateMap<Episode, EpisodeDto>().ReverseMap();

            CreateMap<Characteristic, CharacteristicDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypeItem.Type))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GenderItem.Gender))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.SpeciesItem.Species))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StatusItem.Status))
                .ForMember(dest => dest.CreatedDisplay, opt => opt.MapFrom(src => src.Created.ToString("MM/dd/yyyy h:mm tt")))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(s => s.Location))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(s => s.Origin))
                .ForMember(dest => dest.Episode, opt => opt.MapFrom(s => s.Episodes.ToList()))
                .ReverseMap();
        }
    }
}


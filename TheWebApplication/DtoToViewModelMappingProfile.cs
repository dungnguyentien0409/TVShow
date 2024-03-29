﻿using System;
using AutoMapper;
using ViewModels;
using Dto;

namespace TheWebApplication
{
	public class DtoToViewModelMappingProfile : Profile
	{
		public DtoToViewModelMappingProfile()
		{
            CreateMap<SearchCriteriaDto, SearchCriteriaViewModel>().ReverseMap();
			CreateMap<LocationDto, LocationViewModel>().ReverseMap();
			CreateMap<OriginDto, OriginItemViewModel>().ReverseMap();
            CreateMap<StatusItemDto, StatusItemViewModel>().ReverseMap();
            CreateMap<GenderItemDto, GenderItemViewModel>().ReverseMap();
            CreateMap<TypeItemDto, TypeItemViewModel>().ReverseMap();
            CreateMap<SpeciesItemDto, SpeciesItemViewModel>().ReverseMap();
            CreateMap<EpisodeDto, EpisodeViewModel>()
				.ForMember(dest => dest.EpisodeUrl, opt => opt.MapFrom(s => s.EpisodeUrl))
				.ReverseMap();

            CreateMap<CharacteristicDto, CharacteristicViewModel>()
                .ForMember(dest => dest.LocationItem, opt => opt.MapFrom(s => s.LocationItem))
                .ForMember(dest => dest.OriginItem, opt => opt.MapFrom(s => s.OriginItem))
                .ForMember(dest => dest.SpeciesItem, opt => opt.MapFrom(s => s.SpeciesItem))
                .ForMember(dest => dest.StatusItem, opt => opt.MapFrom(s => s.StatusItem))
                .ForMember(dest => dest.GenderItem, opt => opt.MapFrom(s => s.GenderItem))
                .ForMember(dest => dest.TypeItem, opt => opt.MapFrom(s => s.TypeItem))
                .ForMember(dest => dest.Episodes, opt => opt.MapFrom(s => s.Episodes))
                .ForMember(dest => dest.Statuses, opt => opt.MapFrom(s => s.Statuses))
                .ForMember(dest => dest.Specieses, opt => opt.MapFrom(s => s.Specieses))
                .ForMember(dest => dest.Genders, opt => opt.MapFrom(s => s.Genders))
                .ForMember(dest => dest.Origins, opt => opt.MapFrom(s => s.Origins))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(s => s.Locations))
                .ForMember(dest => dest.Types, opt => opt.MapFrom(s => s.Types))
                .ReverseMap();
		}
	}
}


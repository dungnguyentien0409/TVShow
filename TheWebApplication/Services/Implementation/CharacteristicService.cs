﻿using System;
using AutoMapper;
using Domain.Interfaces;
using Services.Interfaces;
using TheWebApplication.Controllers;
using ViewModels;
using Dto;
using Entities = Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Services.Implementation
{
	public class CharacteristicService : ICharacteristicService
	{
        private readonly ILogger<CharacteristicController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CharacteristicService(ILogger<CharacteristicController> logger, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

        public PagedResponse<List<CharacteristicDto>> GetAllCharacteristic(Guid? locationId, int pageIndex, int pageSize) {
            var results = new List<CharacteristicDto>();
            var query = _unitOfWork.Characteristic.Query();

            if (locationId != Guid.Empty)
            {
                query = query.Where(w => w.LocationId == locationId);
            }

            var totalPage = query.Count() / pageSize + 1;
            var response = query
                .OrderBy(o => o.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();
            var currentItem = pageIndex * pageSize + response.Count();

            for (var i = 0; i < response.Count; i++)
            {
                var item = response[i];
                item.StatusItem = _unitOfWork.Status.GetByIdOrDefault(item.StatusId);
                item.TypeItem = _unitOfWork.Type.GetByIdOrDefault(item.TypeId);
                item.SpeciesItem = _unitOfWork.Species.GetByIdOrDefault(item.SpeciesId);
                item.GenderItem = _unitOfWork.Gender.GetByIdOrDefault(item.GenderId);
                item.Episodes = _unitOfWork.Episode.GetByCharacteristicId(item.Id).ToList();
                item.Location = _unitOfWork.Location.GetByIdOrDefault(item.LocationId);
                item.Origin = _unitOfWork.Origin.GetByIdOrDefault(item.OriginId);

                var dto = _mapper.Map<CharacteristicDto>(item);
                dto.No = pageIndex * pageSize + i + 1;
                results.Add(dto);
            }

            return new PagedResponse<List<CharacteristicDto>>()
            {
                Results = results,
                TotalPage = totalPage,
                PageIndex = pageIndex,
            };
        }

        public CharacteristicDto AddNew()
        {
            var dto = new CharacteristicDto();

            dto.Locations = _unitOfWork.Location.Query()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
            dto.Types = _unitOfWork.Type.Query()
                .Select(s => new SelectListItem
                {
                    Text = s.Type,
                    Value = s.Id.ToString()
                });
            dto.Genders = _unitOfWork.Gender.Query()
                .Select(s => new SelectListItem
                {
                    Text = s.Gender,
                    Value = s.Id.ToString()
                });
            dto.Specieses = _unitOfWork.Species.Query()
                .Select(s => new SelectListItem
                {
                    Text = s.Species,
                    Value = s.Id.ToString()
                });
            dto.Statuses = _unitOfWork.Status.Query()
                .Select(s => new SelectListItem
                {
                    Text = s.Status,
                    Value = s.Id.ToString()
                });
            dto.Origins = _unitOfWork.Origin.Query()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
            dto.Url = "";
            dto.Image = "";

            return dto;
        }

        public bool Create(CharacteristicDto dto)
        {
            try
            {
                var item = _mapper.Map<Entities.Characteristic>(dto);

                _unitOfWork.Characteristic.Add(item);
                _unitOfWork.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when inserting new characteristic", ex);
                return false;
            }
        }

        public SearchCriteriaDto GetSearchCriterias()
        {
            var criteriaDto = new SearchCriteriaDto();

            var locations = _unitOfWork.Location.Query().ToList();
            foreach(var location in locations)
            {
                criteriaDto.Locations.Add(new SelectListItem
                {
                    Text = location.Name,
                    Value = location.Id.ToString()
                });
            }

            return criteriaDto;
        }
    }
}


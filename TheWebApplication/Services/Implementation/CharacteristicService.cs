using System;
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

        public List<CharacteristicDto> GetAllCharacteristic() {
            var results = new List<CharacteristicDto>();

            var response = _unitOfWork.Characteristic.GetAll().ToList();
            foreach (var item in response)
            {
                item.StatusItem = _unitOfWork.Status.GetByIdOrDefault(item.StatusId);
                item.TypeItem = _unitOfWork.Type.GetByIdOrDefault(item.TypeId);
                item.SpeciesItem = _unitOfWork.Species.GetByIdOrDefault(item.SpeciesId);
                item.GenderItem = _unitOfWork.Gender.GetByIdOrDefault(item.GenderId);
                item.Episodes = _unitOfWork.Episode.GetByCharacteristicId(item.Id).ToList();
                item.Location = _unitOfWork.Location.GetByIdOrDefault(item.LocationId);
                item.Origin = _unitOfWork.Origin.GetByIdOrDefault(item.OriginId);

                var dto = _mapper.Map<CharacteristicDto>(item);
                results.Add(dto);
            }

            return results;
        }

        public CharacteristicDto AddNew()
        {
            var dto = new CharacteristicDto();

            dto.Locations = _unitOfWork.Location.GetAll()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
            dto.Types = _unitOfWork.Type.GetAll()
                .Select(s => new SelectListItem
                {
                    Text = s.Type,
                    Value = s.Id.ToString()
                });
            dto.Genders = _unitOfWork.Gender.GetAll()
                .Select(s => new SelectListItem
                {
                    Text = s.Gender,
                    Value = s.Id.ToString()
                });
            dto.Specieses = _unitOfWork.Species.GetAll()
                .Select(s => new SelectListItem
                {
                    Text = s.Species,
                    Value = s.Id.ToString()
                });
            dto.Statuses = _unitOfWork.Status.GetAll()
                .Select(s => new SelectListItem
                {
                    Text = s.Status,
                    Value = s.Id.ToString()
                });
            dto.Origins = _unitOfWork.Origin.GetAll()
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
                item.GenderItem = null;
                item.SpeciesItem = null;
                item.Location = null;
                item.Origin = null;
                item.TypeItem = null;
                item.StatusItem = null;

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
    }
}


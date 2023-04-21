using System;
using Dto;
using ViewModels;
using Entities = Domain.Entities;

namespace Services.Interfaces
{
	public interface ICharacteristicService
	{
        PagedResponse<List<CharacteristicDto>> GetAllCharacteristic(int pageIndex, int pageSize);
		CharacteristicDto AddNew();
		bool Create(CharacteristicDto dto);
	}
}


using System;
using Dto;
using Entities = Domain.Entities;

namespace Services.Interfaces
{
	public interface ICharacteristicService
	{
		List<CharacteristicDto> GetAllCharacteristic();
		CharacteristicDto AddNew();
		bool Create(CharacteristicDto dto);
	}
}


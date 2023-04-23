using DataAccessEF;
using DataAccessEF.UnitOfWork;
using Entities = Domain.Entities;
using Models;
using Helper;

namespace Service
{
	public class DatabaseService
	{
		TVShowContext _context;
		UnitOfWork _unitOfWork;
        string _endpoint;

		public DatabaseService(TVShowContext context, string endpoint)
		{
			_context = context;
			_unitOfWork = new UnitOfWork(context);
            _endpoint = endpoint;
		}

        public void ImportToDatabase()
        {
            var characteristics = new List<Characteristic>();
            var currentUrl = _endpoint;

            CleanUpData();

            while (!string.IsNullOrEmpty(currentUrl))
            {
                (currentUrl, characteristics) = DataHelper.GetDataFromRickAndMorty(currentUrl);
                if (characteristics.Count == 0) continue;

                ImportCurrentBatchToDatabase(characteristics);
            }
        }

		private void ImportCurrentBatchToDatabase(List<Characteristic> characteristics)
		{
			foreach(var characteristic in characteristics)
            {
                var characteristicId = InsertCharacteristic(characteristic);
                characteristicId = characteristicId.HasValue ? characteristicId.Value : Guid.Empty;

                InsertEpisode(characteristic.Episode, characteristicId.Value);
            }
		}

		private void CleanUpData()
		{
            _unitOfWork.Characteristic.RemoveRange(_unitOfWork.Characteristic.Query().ToList());
            _unitOfWork.Episode.RemoveRange(_unitOfWork.Episode.Query().ToList());
            _unitOfWork.Gender.RemoveRange(_unitOfWork.Gender.Query().ToList());
            _unitOfWork.Location.RemoveRange(_unitOfWork.Location.Query().ToList());
            _unitOfWork.Origin.RemoveRange(_unitOfWork.Origin.Query().ToList());
            _unitOfWork.Species.RemoveRange(_unitOfWork.Species.Query().ToList());
            _unitOfWork.Status.RemoveRange(_unitOfWork.Status.Query().ToList());
            _unitOfWork.Type.RemoveRange(_unitOfWork.Type.Query().ToList());
            _unitOfWork.Save();
		}

        private void InsertEpisode(List<string> episodeUrls, Guid charId)
        {
            var episodeItems = new List<Entities.Episode>();
            foreach(var url in episodeUrls)
            {
                var episodeItem = new Entities.Episode();
                episodeItem.CharacteristicId = charId;
                episodeItem.EpisodeUrl = url;

                episodeItems.Add(episodeItem);
            }

            _unitOfWork.Episode.AddRange(episodeItems);
            _unitOfWork.Save();
        }

        private Guid? InsertCharacteristic(Characteristic characteristic)
        {
            var item = new Entities.Characteristic();
            item.Name = characteristic.Name;
            item.StatusId = InsertIfNotExistStatus(characteristic.Status);
            item.SpeciesId = InsertIfNotExistSpecies(characteristic.Species);
            item.TypeId = InsertIfNotExistType(characteristic.Type);
            item.GenderId = InsertIfNotExistGender(characteristic.Gender);
            item.OriginId = InsertIfNotExistOrigin(characteristic.Origin);
            item.LocationId = InsertIfNotExistLocation(characteristic.Location);
            item.Image = characteristic.Image;
            item.Url = characteristic.Url;
            item.Created = characteristic.Created;

            _unitOfWork.Characteristic.Add(item);
            _unitOfWork.Save();

            return item.Id;
        }

		private Guid? InsertIfNotExistStatus(string status)
		{
			var statusItem = _context.Statuses.FirstOrDefault(f => f.Status == status);

			if (statusItem != null) return statusItem.Id;

			statusItem = new Entities.StatusItem();
			statusItem.Status = status;

            _unitOfWork.Status.Add(statusItem);
            _unitOfWork.Save();

			return statusItem.Id;
		}

        private Guid? InsertIfNotExistSpecies(string species)
        {
            var speciesItem = _context.Species.FirstOrDefault(f => f.Species == species);

            if (speciesItem != null) return speciesItem.Id;

            speciesItem = new Entities.SpeciesItem();
            speciesItem.Species = species;

            _unitOfWork.Species.Add(speciesItem);
            _unitOfWork.Save();

            return speciesItem.Id;
        }

        private Guid? InsertIfNotExistType(string type)
        {
            var typeItem = _context.Types.FirstOrDefault(f => f.Type == type);

            if (typeItem != null) return typeItem.Id;

            typeItem = new Entities.TypeItem();
            typeItem.Type = type;

            _unitOfWork.Type.Add(typeItem);
            _unitOfWork.Save();

            return typeItem.Id;
        }

        private Guid? InsertIfNotExistGender(string gender)
        {
            var genderItem = _context.Genders.FirstOrDefault(f => f.Gender == gender);

            if (genderItem != null) return genderItem.Id;

            genderItem = new Entities.GenderItem();
            genderItem.Gender = gender;

            _unitOfWork.Gender.Add(genderItem);
            _unitOfWork.Save();

            return genderItem.Id;
        }

        private Guid? InsertIfNotExistLocation(Location location)
        {
            var locationItem = _context.Locations.FirstOrDefault(f => f.Name == location.Name);

            if (locationItem != null) return locationItem.Id;

            locationItem = new Entities.Location();
            locationItem.Name = location.Name;
            locationItem.Url = location.Url;

            _unitOfWork.Location.Add(locationItem);
            _unitOfWork.Save();

            return locationItem.Id;
        }

        private Guid? InsertIfNotExistOrigin(Origin origin)
        {
            var originItem = _context.Origins.FirstOrDefault(f => f.Name == origin.Name);

            if (originItem != null) return originItem.Id;

            originItem = new Entities.Origin();
            originItem.Name = origin.Name;
            originItem.Url = origin.Url;

            _unitOfWork.Origin.Add(originItem);
            _unitOfWork.Save();

            return originItem.Id;
        }
    }
}


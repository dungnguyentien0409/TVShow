using System;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using TheWebApplication.Controllers;
using ViewModels;

namespace UnitTest
{
    public class CharacteristicServiceTest
	{
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private CharacteristicService _charService;
        private List<GenderItem> genders;
        private List<Location> locations;
        private List<Origin> origins;
        private List<StatusItem> statuses;
        private List<SpeciesItem> specieses;
        private List<TypeItem> types;
        private List<Episode> episodes;
        private DateTime _currentTime;
        private List<Guid> GUIDS = new List<Guid>
        {
            new Guid("ab02ba75-014f-430b-a840-0053a9db7df9"),
            new Guid("f588aa47-99b3-464c-bc02-006a15795bce"),
            new Guid("be157226-9d1e-40a6-bcc8-0099b9c95d76")
        };

        [SetUp]
        public void Setup()
        {
            _currentTime = DateTime.ParseExact("2021-10-25 10:18:19.997",
                "yyyy-MM-dd HH:mm:ss.fff",
                System.Globalization.CultureInfo.InvariantCulture);
            InitData();
            var myProfile = new EntitiesToDtoMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            _charService = new CharacteristicService(Mock.Of<ILogger<CharacteristicController>>(), _unitOfWork.Object, mapper);
        }

        [Test]
        public void GetAllCharacteristic_ReturnAllCharacteristic()
        {
            var expectedResult = new PagedResponse<List<CharacteristicDto>>
            {
                PageIndex = 0,
                TotalPage = 1,
                Results = GetExpectedCharacteristics()
            };
            var actualResult = _charService.GetAllCharacteristic(Guid.Empty, 0, 5);

            AssertObjectsAreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetAllCharacteristicByLocationId_ReturnAllCharacteristicByLocationId()
        {
            var expectedResult = new PagedResponse<List<CharacteristicDto>>
            {
                PageIndex = 0,
                TotalPage = 1,
                Results = GetExpectedCharacteristics().Where(s => s.LocationId == GUIDS[0]).ToList()
            };
            var actualResult = _charService.GetAllCharacteristic(GUIDS[0], 0, 5);

            AssertObjectsAreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AddNewCharacteristic_ReturnMasterDataForAddNewPage()
        {
            var expectedResult = new CharacteristicDto
            {
                Locations = GetExpectedLocations(),
                Types = GetExpectedTypes(),
                Genders = GetExpectedGenders(),
                Specieses = GetExpectedSpecies(),
                Statuses = GetExpectedStatus(),
                Origins = GetExpectedOrigins(),
            };

            var actualResult = _charService.AddNew();

            AssertObjectsAreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CreateCharacteristic_ReturnTrue()
        {
            var charItemDto = new CharacteristicDto
            {
                LocationId = GUIDS[0],
                TypeId = GUIDS[0],
                SpeciesId = GUIDS[0],
                StatusId = GUIDS[0],
                GenderId = GUIDS[0],
                OriginId = GUIDS[0],
                Name = "Test Create Characteristic",
                Url = "Test Url",
                Image = "Test Image",
                EpisodeString = "1,3,4"
            };

            var result = _charService.Create(charItemDto);

            Assert.True(result);
        }

        [Test]
        public void GetSearchCriterias_ReturnCriteria()
        {
            var expectedResult = new SearchCriteriaDto();
            expectedResult.Locations.AddRange(GetExpectedLocations());

            var actualResult = _charService.GetSearchCriterias();

            AssertObjectsAreEqual(expectedResult, actualResult);
        }

        private void InitData()
        {
            
            genders = new List<GenderItem>
            {
                new GenderItem { Id = GUIDS[0], Gender = "Male", Created = _currentTime },
                new GenderItem { Id = GUIDS[1], Gender = "Female", Created = _currentTime },
            };
            locations = new List<Location> {
                new Location { Id = GUIDS[0], Name = "Location_test1", Created = _currentTime },
                new Location { Id = GUIDS[1], Name = "Location_test2", Created = _currentTime },
                new Location { Id = GUIDS[2], Name = "Location_test3", Created = _currentTime }
            };
            origins = new List<Origin> {
                new Origin { Id = GUIDS[0], Name = "Origin_test1", Created = _currentTime },
                new Origin { Id = GUIDS[1], Name = "Origin_test2", Created = _currentTime },
                new Origin { Id = GUIDS[2], Name = "Origin_test3", Created = _currentTime }
            };
            statuses = new List<StatusItem>
            {
                new StatusItem { Id = GUIDS[0], Status = "Alive", Created = _currentTime },
            };
            specieses = new List<SpeciesItem>
            {
                new SpeciesItem { Id = GUIDS[0], Species = "Human", Created = _currentTime },
            };
            types = new List<TypeItem>
            {
                new TypeItem { Id = GUIDS[0], Type = "Cat Person", Created = _currentTime },
            };
            episodes = new List<Episode>
            {
                new Episode { Id = GUIDS[0], CharacteristicId = GUIDS[0], EpisodeUrl = "Test Episode", Created = _currentTime }
            };

            var characteristics = new List<Characteristic>
            {
                new Characteristic
                {
                    Id = GUIDS[0],
                    Name = "Test",
                    LocationId = GUIDS[0],
                    SpeciesId = GUIDS[0],
                    GenderId = GUIDS[0],
                    TypeId = GUIDS[0],
                    OriginId = GUIDS[0],
                    StatusId = GUIDS[0],
                    Url = "Url",
                    Image = "Image",
                    Created = _currentTime
                },
                new Characteristic
                {
                    Id = GUIDS[1],
                    Name = "Test2",
                    LocationId = GUIDS[1],
                    SpeciesId = GUIDS[0],
                    GenderId = GUIDS[0],
                    TypeId = GUIDS[0],
                    OriginId = GUIDS[0],
                    StatusId = GUIDS[0],
                    Url = "Url",
                    Image = "Image",
                    Created = _currentTime
                }
            };

            _unitOfWork.Setup(x => x.Location.Query()).Returns(() => locations.AsQueryable());
            _unitOfWork.Setup(x => x.Species.Query()).Returns(() => specieses.AsQueryable());
            _unitOfWork.Setup(x => x.Gender.Query()).Returns(() => genders.AsQueryable());
            _unitOfWork.Setup(x => x.Type.Query()).Returns(() => types.AsQueryable());
            _unitOfWork.Setup(x => x.Origin.Query()).Returns(() => origins.AsQueryable());
            _unitOfWork.Setup(x => x.Status.Query()).Returns(() => statuses.AsQueryable());
            _unitOfWork.Setup(x => x.Characteristic.Add(It.IsAny<Characteristic>())).Verifiable();
            _unitOfWork.Setup(x => x.Episode.Add(It.IsAny<Episode>())).Verifiable();

            _unitOfWork.SetupSequence(x => x.Location.GetByIdOrDefault(It.IsAny<Guid?>()))
                .Returns(() => locations[0])
                .Returns(() => locations[1]);
            _unitOfWork.Setup(x => x.Species.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => specieses[0]);
            _unitOfWork.Setup(x => x.Gender.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => genders[0]);
            _unitOfWork.Setup(x => x.Type.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => types[0]);
            _unitOfWork.Setup(x => x.Origin.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => origins[0]);
            _unitOfWork.Setup(x => x.Status.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => statuses[0]);
            _unitOfWork.Setup(x => x.Episode.GetByCharacteristicId(It.IsAny<Guid>())).Returns(() => episodes.AsQueryable());


            _unitOfWork.Setup(x => x.Characteristic.Query()).Returns(() => characteristics.AsQueryable());
        }

        private List<CharacteristicDto> GetExpectedCharacteristics()
        {
            return new List<CharacteristicDto>
            {
                new CharacteristicDto
                {
                    Id = GUIDS[0],
                    No = 1,
                    Name = "Test",
                    LocationId = GUIDS[0],
                    LocationItem = new LocationDto { Id = GUIDS[0], Name = "Location_test1" },
                    SpeciesId = GUIDS[0],
                    SpeciesItem = new SpeciesItemDto { Id = GUIDS[0], Species = "Human" },
                    GenderId = GUIDS[0],
                    GenderItem = new GenderItemDto { Id = GUIDS[0], Gender = "Male" },
                    TypeId = GUIDS[0],
                    TypeItem = new TypeItemDto { Id = GUIDS[0], Type = "Cat Person" },
                    OriginId = GUIDS[0],
                    OriginItem = new OriginDto { Id = GUIDS[0], Name = "Origin_test1" },
                    StatusId = GUIDS[0],
                    StatusItem = new StatusItemDto { Id = GUIDS[0], Status = "Alive" },
                    Episodes = new List<EpisodeDto> { new EpisodeDto { EpisodeUrl = "Test Episode" } },
                    Url = "Url",
                    Image = "Image",
                    Created = _currentTime
                },
                new CharacteristicDto
                {
                    Id = GUIDS[1],
                    No = 2,
                    Name = "Test2",
                    LocationId = GUIDS[1],
                    LocationItem = new LocationDto { Id = GUIDS[1], Name = "Location_test2" },
                    SpeciesId = GUIDS[0],
                    SpeciesItem = new SpeciesItemDto { Id = GUIDS[0], Species = "Human" },
                    GenderId = GUIDS[0],
                    GenderItem = new GenderItemDto { Id = GUIDS[0], Gender = "Male" },
                    TypeId = GUIDS[0],
                    TypeItem = new TypeItemDto { Id = GUIDS[0], Type = "Cat Person" },
                    OriginId = GUIDS[0],
                    OriginItem = new OriginDto { Id = GUIDS[0], Name = "Origin_test1" },
                    StatusId = GUIDS[0],
                    StatusItem = new StatusItemDto { Id = GUIDS[0], Status = "Alive" },
                    Episodes = new List<EpisodeDto> { new EpisodeDto { EpisodeUrl = "Test Episode" } },
                    Url = "Url",
                    Image = "Image",
                    Created = _currentTime
                }
            };
        }

        private List<SelectListItem> GetExpectedLocations()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Location_test1", Value = GUIDS[0].ToString() },
                new SelectListItem { Text = "Location_test2", Value = GUIDS[1].ToString() },
                new SelectListItem { Text = "Location_test3", Value = GUIDS[2].ToString() }
            };
        }

        private List<SelectListItem> GetExpectedGenders()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Male", Value = GUIDS[0].ToString() },
                new SelectListItem { Text = "Female", Value = GUIDS[1].ToString() }
            };
        }

        private List<SelectListItem> GetExpectedOrigins()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Origin_test1", Value = GUIDS[0].ToString() },
                new SelectListItem { Text = "Origin_test2", Value = GUIDS[1].ToString() },
                new SelectListItem { Text = "Origin_test3", Value = GUIDS[2].ToString() }
            };
        }

        private List<SelectListItem> GetExpectedTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Cat Person", Value = GUIDS[0].ToString() }
            };
        }

        private List<SelectListItem> GetExpectedStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Alive", Value = GUIDS[0].ToString() }
            };
        }

        private List<SelectListItem> GetExpectedSpecies()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Human", Value = GUIDS[0].ToString() }
            };
        }

        private void AssertObjectsAreEqual(object expectedResult, object actualResult)
        {
            var expectedString = JsonConvert.SerializeObject(expectedResult);
            var actualString = JsonConvert.SerializeObject(actualResult);

            Assert.That(expectedString, Is.EqualTo(actualString));
        }
    }
}


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
                Results = new List<CharacteristicDto>
                {
                    GetExpectedCharacteristic()
                }
            };
            var actualResult = _charService.GetAllCharacteristic(Guid.Empty, 0, 1);

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
                LocationId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                TypeId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                SpeciesId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                StatusId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                GenderId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                OriginId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
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
                new GenderItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Gender = "Male", Created = _currentTime },
                new GenderItem { Id = new Guid("f588aa47-99b3-464c-bc02-006a15795bce"), Gender = "Female", Created = _currentTime },
            };
            locations = new List<Location> {
                new Location { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Name = "Location_test1", Created = _currentTime },
                new Location { Id = new Guid("f588aa47-99b3-464c-bc02-006a15795bce"), Name = "Location_test2", Created = _currentTime },
                new Location { Id = new Guid("be157226-9d1e-40a6-bcc8-0099b9c95d76"), Name = "Location_test3", Created = _currentTime }
            };
            origins = new List<Origin> {
                new Origin { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Name = "Origin_test1", Created = _currentTime },
                new Origin { Id = new Guid("f588aa47-99b3-464c-bc02-006a15795bce"), Name = "Origin_test2", Created = _currentTime },
                new Origin { Id = new Guid("be157226-9d1e-40a6-bcc8-0099b9c95d76"), Name = "Origin_test3", Created = _currentTime }
            };
            statuses = new List<StatusItem>
            {
                new StatusItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Status = "Alive", Created = _currentTime },
            };
            specieses = new List<SpeciesItem>
            {
                new SpeciesItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Species = "Human", Created = _currentTime },
            };
            types = new List<TypeItem>
            {
                new TypeItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Type = "Cat Person", Created = _currentTime },
            };
            episodes = new List<Episode>
            {
                new Episode { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), CharacteristicId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), EpisodeUrl = "Test Episode", Created = _currentTime }
            };

            var characteristics = new List<Characteristic>
            {
                new Characteristic
                {
                    Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                    Name = "Test",
                    LocationId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                    SpeciesId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                    GenderId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                    TypeId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                    OriginId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                    StatusId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
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

            _unitOfWork.Setup(x => x.Location.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => locations[0]);
            _unitOfWork.Setup(x => x.Species.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => specieses[0]);
            _unitOfWork.Setup(x => x.Gender.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => genders[0]);
            _unitOfWork.Setup(x => x.Type.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => types[0]);
            _unitOfWork.Setup(x => x.Origin.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => origins[0]);
            _unitOfWork.Setup(x => x.Status.GetByIdOrDefault(It.IsAny<Guid?>())).Returns(() => statuses[0]);
            _unitOfWork.Setup(x => x.Episode.GetByCharacteristicId(It.IsAny<Guid>())).Returns(() => episodes.AsQueryable());


            _unitOfWork.Setup(x => x.Characteristic.Query()).Returns(() => characteristics.AsQueryable());
        }

        private CharacteristicDto GetExpectedCharacteristic()
        {
            return new CharacteristicDto
            {
                Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                No = 1,
                Name = "Test",
                LocationId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                LocationItem = new LocationDto { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Name = "Location_test1" },
                SpeciesId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                SpeciesItem = new SpeciesItemDto { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Species = "Human" },
                GenderId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                GenderItem = new GenderItemDto { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Gender = "Male" },
                TypeId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                TypeItem = new TypeItemDto { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Type = "Cat Person" },
                OriginId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                OriginItem = new OriginDto { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Name = "Origin_test1" },
                StatusId = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"),
                StatusItem = new StatusItemDto { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Status = "Alive" },
                Episodes = new List<EpisodeDto> { new EpisodeDto { EpisodeUrl = "Test Episode" } },
                Url = "Url",
                Image = "Image",
                Created = _currentTime
            };
        }

        private List<SelectListItem> GetExpectedLocations()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Location_test1", Value = "fd7a1d06-4e19-41a3-a335-00014185348b" },
                new SelectListItem { Text = "Location_test2", Value = "f588aa47-99b3-464c-bc02-006a15795bce" },
                new SelectListItem { Text = "Location_test3", Value = "be157226-9d1e-40a6-bcc8-0099b9c95d76" }
            };
        }

        private List<SelectListItem> GetExpectedGenders()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Male", Value = "fd7a1d06-4e19-41a3-a335-00014185348b" },
                new SelectListItem { Text = "Female", Value = "f588aa47-99b3-464c-bc02-006a15795bce" }
            };
        }

        private List<SelectListItem> GetExpectedOrigins()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Origin_test1", Value = "fd7a1d06-4e19-41a3-a335-00014185348b" },
                new SelectListItem { Text = "Origin_test2", Value = "f588aa47-99b3-464c-bc02-006a15795bce" },
                new SelectListItem { Text = "Origin_test3", Value = "be157226-9d1e-40a6-bcc8-0099b9c95d76" }
            };
        }

        private List<SelectListItem> GetExpectedTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Cat Person", Value = "fd7a1d06-4e19-41a3-a335-00014185348b" }
            };
        }

        private List<SelectListItem> GetExpectedStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Alive", Value = "fd7a1d06-4e19-41a3-a335-00014185348b" }
            };
        }

        private List<SelectListItem> GetExpectedSpecies()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Human", Value = "fd7a1d06-4e19-41a3-a335-00014185348b" }
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


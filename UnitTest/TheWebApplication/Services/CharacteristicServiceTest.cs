namespace UnitTest
{
    public class CharacteristicServiceTest
	{
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private CharacteristicService _charService;

        [SetUp]
        public void Setup()
        {
            InitMasterData();
            _charService = new CharacteristicService(null, _unitOfWork.Object, null);
        }

        [Test]
        public void GetAllCharacteristic_ReturnAllCharacteristic()
        {

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

        }

        [Test]
        public void GetSearchCriterias_ReturnCriteria()
        {
            var expectedResult = new SearchCriteriaDto();
            expectedResult.Locations.AddRange(GetExpectedLocations());

            var actualResult = _charService.GetSearchCriterias();

            AssertObjectsAreEqual(expectedResult, actualResult);
        }

        private void InitMasterData()
        {
            var genders = new List<GenderItem>
            {
                new GenderItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Gender = "Male", Created = DateTime.Now },
                new GenderItem { Id = new Guid("f588aa47-99b3-464c-bc02-006a15795bce"), Gender = "Female", Created = DateTime.Now },
            };
            var locations = new List<Location> {
                new Location { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Name = "Location_test1", Created = DateTime.Now },
                new Location { Id = new Guid("f588aa47-99b3-464c-bc02-006a15795bce"), Name = "Location_test2", Created = DateTime.Now },
                new Location { Id = new Guid("be157226-9d1e-40a6-bcc8-0099b9c95d76"), Name = "Location_test3", Created = DateTime.Now }
            };
            var origins = new List<Origin> {
                new Origin { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Name = "Origin_test1", Created = DateTime.Now },
                new Origin { Id = new Guid("f588aa47-99b3-464c-bc02-006a15795bce"), Name = "Origin_test2", Created = DateTime.Now },
                new Origin { Id = new Guid("be157226-9d1e-40a6-bcc8-0099b9c95d76"), Name = "Origin_test3", Created = DateTime.Now }
            };
            var status = new List<StatusItem>
            {
                new StatusItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Status = "Alive", Created = DateTime.Now },
            };
            var species = new List<SpeciesItem>
            {
                new SpeciesItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Species = "Human", Created = DateTime.Now },
            };
            var types = new List<TypeItem>
            {
                new TypeItem { Id = new Guid("fd7a1d06-4e19-41a3-a335-00014185348b"), Type = "Cat Person", Created = DateTime.Now },
            };

            _unitOfWork.Setup(x => x.Location.Query()).Returns(() => locations.AsQueryable());
            _unitOfWork.Setup(x => x.Species.Query()).Returns(() => species.AsQueryable());
            _unitOfWork.Setup(x => x.Gender.Query()).Returns(() => genders.AsQueryable());
            _unitOfWork.Setup(x => x.Type.Query()).Returns(() => types.AsQueryable());
            _unitOfWork.Setup(x => x.Origin.Query()).Returns(() => origins.AsQueryable());
            _unitOfWork.Setup(x => x.Status.Query()).Returns(() => status.AsQueryable());
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


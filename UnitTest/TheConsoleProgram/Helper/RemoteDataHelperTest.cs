using System;
using Helper;
using Models;

namespace UnitTest
{
	public class RemoteDataHelperTest
	{
		string _characteristicUrl = "";
		IRemoteDataHelper<Models.Characteristic> _helper;
        [SetUp]
		public void SetUp()
		{
			_characteristicUrl = "https://rickandmortyapi.com/api/character/";
			_helper = new RemoteDataHelper<Models.Characteristic>();
        }

		[Test]
		public void GetAndParseData_ReturnEmptyDataObject()
		{
			var data = _helper.GetAndParseData("");

			Assert.That(data, Is.TypeOf<RemoteDataResponse<Models.Characteristic>>());
			Assert.That(data.Results.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetAndParseData_ReturnCorrectDataType()
        {
            var data = _helper.GetAndParseData(_characteristicUrl);

            Assert.That(data, Is.TypeOf<RemoteDataResponse<Models.Characteristic>>());
        }
    }
}


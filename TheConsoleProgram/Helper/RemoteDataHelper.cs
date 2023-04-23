using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Helper
{
	public static class RemoteDataHelper
	{
		public static RemoteDataResponse GetAndParseData(string url)
		{
            var remoteDataResponse = new RemoteDataResponse();

            try
            {
                var client = new HttpClient();
                var webRequest = new HttpRequestMessage(HttpMethod.Get, url);
                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());
                var jsonResponse = reader.ReadToEnd();

                remoteDataResponse = JsonConvert.DeserializeObject<RemoteDataResponse>(jsonResponse);

                if (remoteDataResponse == null) return new RemoteDataResponse();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return remoteDataResponse;
        }

        public static RemoteDataResponse FilterDataByStatus(RemoteDataResponse data, string status)
        {
            if (data == null || data.Results == null) return data;

            data.Results = data.Results
                .Where(w => w.Status == status)
                .ToList();

            return data;
        }
	}
}


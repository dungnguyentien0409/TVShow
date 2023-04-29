using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Helper
{
	public class RemoteDataHelper<T> : IRemoteDataHelper<T> where T : class
	{
		public RemoteDataResponse<T> GetAndParseData(string url)
		{
            var remoteDataResponse = new RemoteDataResponse<T>();

            try
            {
                var client = new HttpClient();
                var webRequest = new HttpRequestMessage(HttpMethod.Get, url);
                var response = client.Send(webRequest);

                using var reader = new StreamReader(response.Content.ReadAsStream());
                var stringResponse = reader.ReadToEnd();

                remoteDataResponse = JsonConvert.DeserializeObject<RemoteDataResponse<T>>(stringResponse);

                if (remoteDataResponse == null) return new RemoteDataResponse<T>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return remoteDataResponse;
        }
	}
}


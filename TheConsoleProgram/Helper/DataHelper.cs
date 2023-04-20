using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Helper
{
	public static class DataHelper
	{
        public const string ACTIVE_STATUS = "Alive";

		public static (string, List<Characteristic>) GetDataFromRickAndMorty(string url)
		{
            var client = new HttpClient();
            var webRequest = new HttpRequestMessage(HttpMethod.Get, url);
            var response = client.Send(webRequest);

            using var reader = new StreamReader(response.Content.ReadAsStream());
            var jsonResponse = reader.ReadToEnd();
            var responseObject = new Response();

            if (jsonResponse != null)
            {
                try
                {
                    responseObject = JsonConvert.DeserializeObject<Response>(jsonResponse);
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }

            var nextUrl = responseObject != null && responseObject.Info != null ?
                responseObject.Info.Next : string.Empty;
            var characteristics = responseObject != null && responseObject.Results != null ?
                responseObject.Results.Where(w => w.Status == ACTIVE_STATUS).ToList() : new List<Characteristic>();

            return (nextUrl, characteristics);
        }
	}
}


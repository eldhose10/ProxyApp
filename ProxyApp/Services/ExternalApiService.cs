using System.Net;

namespace ProxyApp.Services
{
    public class ExternalApiService : IExternalApiService
    {
        public string GetResource(string resource)
        {
            var host = $"https://swapi.dev/api/{resource}";
            return GetDataFromApi(host);
        }

        public string GetResourceById(string resource, int id)
        {
            var host = $"https://swapi.dev/api/{resource}/{id}";
            return GetDataFromApi(host);
        }

        private string GetDataFromApi(string host)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(host);
            }
        }
    }
}

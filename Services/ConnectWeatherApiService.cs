using System;
using RestSharp;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public class ConnectWeatherApiService
    {
        private readonly IRestClient _restClient;

        public ConnectWeatherApiService(IRestClient restClient)
        {
            _restClient = restClient ?? throw new ArgumentNullException(nameof(restClient));
        }

        public async Task<ResponseWeather> ConsumeWeatherApiService(string[] cities)
        {
            if (!ValidRequest(cities))
                return RequesIsNotValid();

            var responseList = new ResponseWeather();

            var sbLog = new StringBuilder();

            foreach (var city in cities)
            {
                _restClient.BaseUrl = new Uri($"http://api.openweathermap.org/data/2.5/forecast?id={city}");
                var request = new RestRequest(Method.GET);

                var resp = await _restClient.ExecuteGetAsync<IRestResponse>(request);

                if (resp == null)
                    continue;

                Console.WriteLine(resp.Content);
                if (string.IsNullOrEmpty(resp.Content))
                    sbLog.AppendLine($"Content is empty; Request idCity: {city}");

                var weather = JsonConvert.DeserializeObject<Weathers>(resp.Content);
                responseList.WeathersList.Add(weather);
            }

            responseList.MessageResponse = (string.IsNullOrEmpty(sbLog.ToString()) ? "Success" : sbLog.ToString());
            responseList.Success = true;

            return responseList;
        }

        private bool ValidRequest(string[] cities)
        {
            return cities != null && cities.Length > 0;
        }

        private ResponseWeather RequesIsNotValid()
        {
            return new ResponseWeather
            {
                MessageResponse = "não informado cidades válidas no request",
                Success = false
            };
        }
    }
}

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

        public async Task<WeathersList> ConsumeWeatherApiService(string[] cities)
        {
            if (!ValidRequest(cities))
                return RequesIsNotValid();

            var weathersList = new WeathersList();

            var sbLog = new StringBuilder();

            foreach (var city in cities)
            {
                _restClient.BaseUrl = new Uri($"http://localhost:56297/api/WeatherForecast/{city}");
                var request = new RestRequest(Method.GET);

                var resp = await _restClient.ExecuteGetAsync<IRestResponse>(request);

                if (resp == null)
                {
                    sbLog.AppendLine($"error: response is null; Request idCity: {city}");
                    continue;
                };

                Console.WriteLine(resp.Content);
                if (string.IsNullOrEmpty(resp.Content) || resp.Content == null)
                    sbLog.AppendLine($"Content is empty - statusCode: {resp.StatusCode}, message: {resp.ErrorMessage}; Request idCity: {city}");
                else
                    weathersList = JsonConvert.DeserializeObject<WeathersList>(resp.Content);
            }

            SetHesponseHeadersCustom(weathersList, sbLog);
            return weathersList;
        }

        private static void SetHesponseHeadersCustom(WeathersList responseList, StringBuilder sbLog)
        {
            bool logIsNull = string.IsNullOrEmpty(sbLog.ToString());
            responseList.MessageResponse = (logIsNull ? "Success" : sbLog.ToString());
        }

        private bool ValidRequest(string[] cities)
        {
            return cities != null && cities.Length > 0;
        }

        private WeathersList RequesIsNotValid()
        {
            return new WeathersList
            {
                MessageResponse = "não informado cidades válidas no request"
            };
        }
    }
}

using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public interface IWeatherService
    {
        List<WeathersList> GetWeathersCity(string cityCode);
        WeathersList Create(WeathersList apiTest);
        void Update(string id, WeathersList apiTestIn);
        void Remove(WeathersList apiTestIn);
        void Remove(string id);
    }
}
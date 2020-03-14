using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public interface IWeatherService
    {
        List<Weathers> GetWeathersCity(string cityCode);
        Weathers Create(Weathers apiTest);
        void Update(string id, Weathers apiTestIn);
        void Remove(Weathers apiTestIn);
        void Remove(string id);
    }
}
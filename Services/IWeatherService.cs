using WeatherDB.Models;

namespace WeatherDB.Services
{
    public interface IWeatherService
    {
        Weather Get(string id);
        Weather Create(Weather apiTest);
        void Update(string id, Weather apiTestIn);
        void Remove(Weather apiTestIn);
        void Remove(string id);
    }
}
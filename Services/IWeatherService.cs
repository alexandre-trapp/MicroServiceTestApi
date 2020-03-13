using WeatherDB.Models;

namespace WeatherDB.Services
{
    public interface IWeatherService
    {
        Weathers Get(string id);
        Weathers Create(Weathers apiTest);
        void Update(string id, Weathers apiTestIn);
        void Remove(Weathers apiTestIn);
        void Remove(string id);
    }
}
using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMongoCollection<WeathersList> WeathersColl;

        public WeatherService(IWeatherDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            WeathersColl = database.GetCollection<WeathersList>(settings.WeatherCollectionName);
        }

        public List<WeathersList> Get() =>
            WeathersColl.Find(x => true).ToList();

        public List<WeathersList> GetWeathersCity(string cityCode) =>
            WeathersColl.Find<WeathersList>(apiTest => apiTest.City.id == cityCode).ToList();

        public WeathersList Create(WeathersList apiTest)
        {
            WeathersColl.InsertOne(apiTest);
            return apiTest;
        }

        public void Update(string id, WeathersList apiTestIn) =>
            WeathersColl.ReplaceOne(apiTest => apiTest._Id == id, apiTestIn);

        public void Remove(WeathersList apiTestIn) =>
            WeathersColl.DeleteOne(apiTest => apiTest._Id == apiTestIn._Id);

        public void Remove(string id) =>
            WeathersColl.DeleteOne(apiTest => apiTest._Id == id);
    }
}
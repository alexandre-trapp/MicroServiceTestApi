using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMongoCollection<Weather> WeatherColl;

        public WeatherService(IWeatherDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            WeatherColl = database.GetCollection<Weather>(settings.WeatherCollectionName);
        }

        public List<Weather> Get() =>
            WeatherColl.Find(x => true).ToList();

        public Weather Get(string id) =>
            WeatherColl.Find<Weather>(apiTest => apiTest.Id == id).FirstOrDefault();

        public Weather Create(Weather apiTest)
        {
            WeatherColl.InsertOne(apiTest);
            return apiTest;
        }

        public void Update(string id, Weather apiTestIn) =>
            WeatherColl.ReplaceOne(apiTest => apiTest.Id == id, apiTestIn);

        public void Remove(Weather apiTestIn) =>
            WeatherColl.DeleteOne(apiTest => apiTest.Id == apiTestIn.Id);

        public void Remove(string id) =>
            WeatherColl.DeleteOne(apiTest => apiTest.Id == id);
    }
}
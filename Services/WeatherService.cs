using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMongoCollection<Weathers> WeathersColl;

        public WeatherService(IWeatherDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            WeathersColl = database.GetCollection<Weathers>(settings.WeatherCollectionName);
        }

        public List<Weathers> Get() =>
            WeathersColl.Find(x => true).ToList();

        public List<Weathers> GetWeathersCity(string cityCode) =>
            WeathersColl.Find<Weathers>(apiTest => apiTest.City.id == cityCode).ToList();

        public Weathers Create(Weathers apiTest)
        {
            WeathersColl.InsertOne(apiTest);
            return apiTest;
        }

        public void Update(string id, Weathers apiTestIn) =>
            WeathersColl.ReplaceOne(apiTest => apiTest._Id == id, apiTestIn);

        public void Remove(Weathers apiTestIn) =>
            WeathersColl.DeleteOne(apiTest => apiTest._Id == apiTestIn._Id);

        public void Remove(string id) =>
            WeathersColl.DeleteOne(apiTest => apiTest._Id == id);
    }
}
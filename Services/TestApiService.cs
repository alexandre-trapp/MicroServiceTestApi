using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;
using MicroServiceTestApi.Models;

namespace MicroServiceTestApi.Services
{
    public class TestApiService
    {
        private readonly IMongoCollection<TestApi> _testApi;
        
        public TestApiService(ITestApiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _testApi = database.GetCollection<TestApi>(settings.TestApiCollectionName);
        }

        public List<TestApi> Get() =>
            _testApi.Find(x => true).ToList();

        public TestApi Get(string id) =>
            _testApi.Find<TestApi>(apiTest => apiTest.Id == id).FirstOrDefault();

        public TestApi Create(TestApi apiTest)
        {
            _testApi.InsertOne(apiTest);
            return apiTest;
        }

        public void Update(string id, TestApi apiTestIn) =>
            _testApi.ReplaceOne(apiTest => apiTest.Id == id, apiTestIn);

        public void Remove(TestApi apiTestIn) =>
            _testApi.DeleteOne(apiTest => apiTest.Id == apiTestIn.Id);

        public void Remove(string id) =>
            _testApi.DeleteOne(apiTest => apiTest.Id == id);
    }
}
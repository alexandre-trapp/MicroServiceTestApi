namespace MicroServiceTestApi.Models
{
    public interface ITestApiDatabaseSettings
    {
        string TestApiCollectionName { get; set; }
        string ConnectionString { get; set; } 
        string DatabaseName { get; set; }
    }

    public class TestApiDatabaseSettings : ITestApiDatabaseSettings
    {
        public string TestApiCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

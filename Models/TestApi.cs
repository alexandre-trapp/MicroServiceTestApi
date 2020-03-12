using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroServiceTestApi.Models
{
    [JsonObject("TestApi")]
    public class TestApi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("Id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string BookName { get; set; }

        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("Author")]
        public string Author { get; set; }
    }
}

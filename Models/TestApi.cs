using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroServiceTestApi.Models
{
    public class TestApi
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string BookName { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}

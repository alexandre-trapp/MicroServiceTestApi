using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherDB.Models
{
    public class Coordinates
    {
        [BsonElement("lat")]
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [BsonElement("lon")]
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
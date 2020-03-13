using MongoDB.Bson;
using Newtonsoft.Json;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherDB.Models
{
    [JsonObject("Weather")]
    public class Weather
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [BsonElement("state")]
        [JsonProperty("state")]
        public string State { get; set; }

        [BsonElement("country")]
        [JsonProperty("country")]
        public string Country { get; set; }

        [BsonElement("lat")]
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [BsonElement("lon")]
        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}

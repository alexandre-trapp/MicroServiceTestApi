using MongoDB.Bson;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherDB.Models
{
    public class Cities
    {
        /// <summary>
        /// unique _id generated from mongoDb
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        /// <summary>
        /// id of the city from api weather
        /// </summary>
        [BsonElement("id")]
        [JsonProperty("id")]
        public string id { get; set; }

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("country")]
        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("timezone")]
        [JsonProperty("timezone")]
        public long Timezone { get; set; }

        [BsonElement("sunrise")]
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [BsonElement("sunset")]
        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        public Coordinates Coord { get; set; }
    }

    public class Coordinates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        [BsonElement("lat")]
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [BsonElement("lon")]
        [JsonProperty("lon")]
        public double Long { get; set; } 
    }

    public static class Serialize
    {
        public static string ToJson(this Cities self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}


using System;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherDB.Models
{
    public class WeathersList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        [BsonElement("cod")]
        [JsonProperty("cod")]
        public long Cod { get; set; }

        [BsonElement("message")]
        [JsonProperty("message")]
        public long Message { get; set; }

        [BsonElement("cnt")]
        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [BsonElement("list")]
        [JsonProperty("list")]
        public ListWeathers[] List { get; set; }

        [BsonElement("city")]
        [JsonProperty("city")]
        public Cities City { get; set; }

        [JsonProperty("messageResponse")]
        public string MessageResponse { get; set; }
    }

    public class ListWeathers
    {
        [BsonElement("dt")]
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [BsonElement("wind")]
        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [BsonElement("dt_txt")]
        [JsonProperty("dt_txt")]
        public string DtTxt { get; set; }

        [BsonElement("sys")]
        [JsonProperty("sys")]
        public SystemData Sys { get; set; }

        [BsonElement("main")]
        [JsonProperty("main")]
        public AtmosphericData Main { get; set; }

        [BsonElement("weather")]
        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [BsonElement("clouds")]
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
    }

    public class Clouds
    {
        [BsonElement("all")]
        [JsonProperty("all")]
        public long All { get; set; }
    }

    public class AtmosphericData
    {
        [BsonElement("temp")]
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [BsonElement("feels_like")]
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [BsonElement("temp_min")]
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [BsonElement("temp_max")]
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        [BsonElement("pressure")]
        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [BsonElement("sea_level")]
        [JsonProperty("sea_level")]
        public long SeaLevel { get; set; }

        [BsonElement("grnd_level")]
        [JsonProperty("grnd_level")]
        public long GrndLevel { get; set; }

        [BsonElement("humidity")]
        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [BsonElement("temp_kf")]
        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }

    public partial class SystemData
    {
        [BsonElement("pod")]
        [JsonProperty("pod")]
        public string Pod { get; set; }
    }

    public class Weather
    {
        [BsonElement("id")]
        [JsonProperty("id")]
        public long Id { get; set; }

        [BsonElement("main")]
        [JsonProperty("main")]
        public string Main { get; set; }

        [BsonElement("description")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [BsonElement("icon")]
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public partial class Wind
    {
        [BsonElement("speed")]
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [BsonElement("deg")]
        [JsonProperty("deg")]
        public long Deg { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}

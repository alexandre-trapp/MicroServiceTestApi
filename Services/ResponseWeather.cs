using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WeatherDB.Models;

namespace WeatherDB.Services
{
    public class ResponseWeather
    {
        [JsonProperty("wheatersList")]
        public List<Weathers> WeathersList { get; set; } = new List<Weathers>();

        [JsonProperty("messageResponse")]
        public string MessageResponse { get; set; }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WeatherDB.Models;
using WeatherDB.Services;

namespace WeatherDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _apiService;

        public WeatherController(WeatherService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            try
            {
                var retorno = _apiService.Get();
                return Ok(Utf8Json.JsonSerializer.ToJsonString(retorno));
            }
            catch (TimeoutException e)
            {
                return NotFound($"Não foi possível conectar ao servidor do banco de dados MongoDb, verifique se o serviço está ativo e estável no servidor. - Erro: {e.Message}");
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        } 

        [HttpGet("{id:length(24)}")]
        public IActionResult Get([FromRoute] string id)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            return Ok(Utf8Json.JsonSerializer.ToJsonString(api));
        }

        [HttpPost("{id:length(24)}")]
        public IActionResult Update([FromRoute] string id, [FromBody] Weathers apiIn)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            _apiService.Update(id, apiIn);
            return Ok("{ mensagem: Item atualizado com sucesso. }");
        }

        [HttpPost("{id:length(24)}")]
        public IActionResult Post([FromRoute] string id, [FromBody] Weathers apiIn)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            _apiService.Create(apiIn);
            return Ok("{ mensagem: Item criado com sucesso. }");
        }

        [HttpPost]
        [Route("search/{city}")]
        public IActionResult SerchWeatherCityApi([FromRoute] string city)
        {

            var _apiService = new ConnectWeatherApiService(new RestClient());
            var response = _apiService.ConsumeWeatherApiService(new string[1] { city });
            
            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost]
        [Route("search_cities/{cities}")]
        public async Task<IActionResult> SerchWeatherCitiesApi([FromRoute] string cities)
        {
            string[] citiesArr = GetCitiesSplitedWithSeparator(cities);
            var _apiService = new ConnectWeatherApiService(new RestClient());

            var response = await _apiService.ConsumeWeatherApiService(citiesArr);
            return ValidateResponseWeather(response);
        }

        private IActionResult ValidateResponseWeather(ResponseWeather response)
        {
            if (response == null)
                return NoContent();

            if (response.Success)
                return Ok(JsonConvert.SerializeObject(response));
            else
                return BadRequest(JsonConvert.SerializeObject(response));
        }

        private static string[] GetCitiesSplitedWithSeparator(string cities)
        {
            var citiesArr = cities.Split(',');
            if (citiesArr.Length == 0)
                citiesArr = cities.Split(';');

            return citiesArr;
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete([FromRoute] string id)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            _apiService.Remove(api);

            return Ok("{ mensagem: Item removido com sucesso. }");
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MicroServiceTestApi.Models;
using MicroServiceTestApi.Services;

namespace MicroServiceTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly TestApiService _apiService;

        public TestApiController(TestApiService apiService)
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

        [HttpGet("{id:length(24)}", Name = "GetTestApi")]
        public ActionResult Get(string id)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            return Ok(Utf8Json.JsonSerializer.ToJsonString(api));
        }

        [HttpPost]
        public ActionResult Create(TestApi api)
        {
            _apiService.Create(api);

            var retorno = CreatedAtRoute("Create", new { id = api.Id.ToString() }, api);
            return Ok("{ mensagem: Item " + api.Id.ToString() + " cadastrado com sucesso. }");
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, TestApi apiIn)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            _apiService.Update(id, apiIn);
            return Ok("{ mensagem: Item " + id + " atualizado com sucesso. }");
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            _apiService.Remove(api.Id);

            return Ok("{ mensagem: Item " + id + " removido com sucesso. }");
        }
    }
}
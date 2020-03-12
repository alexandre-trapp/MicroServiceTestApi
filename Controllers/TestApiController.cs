using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MicroServiceTestApi.Models;
using MicroServiceTestApi.Services;
using System;

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
        public ActionResult<List<TestApi>> Get() 
        {
            try
            {
                return _apiService.Get();
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
        public ActionResult<TestApi> Get(string id)
        {
            var api = _apiService.Get(id);

            if (api == null)
            {
                return NotFound();
            }

            return api;
        }

        [HttpPost]
        public ActionResult<TestApi> Create(TestApi api)
        {
            _apiService.Create(api);

            return CreatedAtRoute("GetTestApi", new { id = api.Id.ToString() }, api);
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

            return NoContent();
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

            return NoContent();
        }
    }
}
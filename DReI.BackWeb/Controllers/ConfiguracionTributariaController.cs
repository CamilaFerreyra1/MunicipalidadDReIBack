using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Data;
using System.Linq;


namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/configuracion-tributaria")]
    public class ConfiguracionTributariaController : ApiController
    {
        private readonly ConfiguracionTributariaService _service;

        public ConfiguracionTributariaController(ConfiguracionTributariaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var lista = _service.ObtenerLista();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetPorId(int id)
        {
            var config = _service.ObtenerPorId(id);
            if (config == null)
                return NotFound();

            return Ok(config);
        }
    }
}
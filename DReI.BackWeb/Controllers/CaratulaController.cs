using System;
using System.Web.Http;
using DReI.BackWeb.Services;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/caratulas")]
    public class CaratulasController : ApiController
    {
        private readonly CaratulaService _service;

        public CaratulasController(CaratulaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var caratulas = _service.ObtenerCaratulas();
            return Ok(caratulas);
        }

        [HttpGet]
        [Route("por-ano/{año}")]
        public IHttpActionResult GetPorAño(int año)
        {
            var caratulas = _service.ObtenerCaratulas(año);
            return Ok(caratulas);
        }

        [HttpGet]
        [Route("desde")]
        public IHttpActionResult GetDesde(DateTime fecha)
        {
            var caratulas = _service.ObtenerCaratulas(fecha);
            return Ok(caratulas);
        }

        [HttpGet]
        [Route("entre")]
        public IHttpActionResult GetEntre(DateTime desde, DateTime hasta)
        {
            var caratulas = _service.ObtenerCaratulas(desde, hasta);
            return Ok(caratulas);
        }
    }
}

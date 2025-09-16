using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/caratulas")]
    public class CaratulasController : ApiController
    {
        // GET: api/caratulas → carátulas del año en curso
        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas();
            return Ok(caratulas);
        }

        // GET: api/caratulas/por-ano/2024 → carátulas de un año
        [HttpGet]
        [Route("por-ano/{año}")]
        public IHttpActionResult GetPorAño(int año)
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas(año);
            return Ok(caratulas);
        }

        // GET: api/caratulas/desde?fecha=2024-06-01 → carátulas desde una fecha
        [HttpGet]
        [Route("desde")]
        public IHttpActionResult GetDesde(DateTime fecha)
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas(fecha);
            return Ok(caratulas);
        }

        // GET: api/caratulas/entre?desde=2024-01-01&hasta=2024-12-31 → carátulas entre fechas
        [HttpGet]
        [Route("entre")]
        public IHttpActionResult GetEntre(DateTime desde, DateTime hasta)
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas(desde, hasta);
            return Ok(caratulas);
        }
    }
}
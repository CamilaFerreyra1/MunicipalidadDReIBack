using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/caratulas")]
    public class CaratulasController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas();
            return Ok(caratulas);
        }

        [HttpGet]
        [Route("por-ano/{año}")]
        public IHttpActionResult GetPorAño(int año)
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas(año);
            return Ok(caratulas);
        }

        [HttpGet]
        [Route("desde")]
        public IHttpActionResult GetDesde(DateTime fecha)
        {
            var service = new CaratulaService();
            var caratulas = service.ObtenerCaratulas(fecha);
            return Ok(caratulas);
        }

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
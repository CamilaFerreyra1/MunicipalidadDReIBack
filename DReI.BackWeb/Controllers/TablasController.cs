using System;
using System.Web.Http;
using System.Linq;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Responses;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/tablas")]
    public class TablasController : ApiController
    {
        private readonly TablasService _service;

        public TablasController(TablasService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("rubros")]
        public IHttpActionResult ObtenerRubros(DateTime fDesde, DateTime fHasta)
        {
            var respuesta = new Respuesta();

            try
            {
                var rubros = _service.Rubros(fDesde, fHasta).ToList();
                respuesta.Resultado = rubros;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }

        [HttpGet]
        [Route("actividades")]
        public IHttpActionResult ObtenerActividades(DateTime fDesde, DateTime fHasta)
        {
            var respuesta = new Respuesta();

            try
            {
                var actividades = _service.Actividades(fDesde, fHasta).ToList();
                respuesta.Resultado = actividades;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }

        [HttpGet]
        [Route("contribuyentes")]
        public IHttpActionResult ObtenerContribuyentes(DateTime fDesde, DateTime fHasta)
        {
            var respuesta = new Respuesta();

            try
            {
                var contribuyentes = _service.Contribuyentes(fDesde, fHasta).ToList();
                respuesta.Resultado = contribuyentes;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }

        [HttpGet]
        [Route("rubros-actividades")]
        public IHttpActionResult ObtenerRubrosActividades(DateTime fDesde, DateTime fHasta)
        {
            var respuesta = new Respuesta();

            try
            {
                var rubrosActividades = _service.RubrosActividades(fDesde, fHasta).ToList();
                respuesta.Resultado = rubrosActividades;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }

        [HttpGet]
        [Route("rubros-contrib")]
        public IHttpActionResult ObtenerRubrosContrib(DateTime fDesde, DateTime fHasta)
        {
            var respuesta = new Respuesta();

            try
            {
                var rubrosContrib = _service.RubrosContrib(fDesde, fHasta).ToList();
                respuesta.Resultado = rubrosContrib;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }
    }
}
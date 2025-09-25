using System;
using System.Net;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Responses;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/test")]
    public class ContribuyenteTestController : ApiController 
    {
        private readonly ContribuyentesService _service; 

        public ContribuyenteTestController(ContribuyentesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("contribuyente/{cuenta:int}")]
        public IHttpActionResult BuscarContribuyente(int cuenta)
        {
            var respuesta = new Respuesta();

            try
            {
                var contribuyente = _service.Obtener(cuenta);

                if (contribuyente == null)
                {
                    respuesta.AgregarMensajeDeError($"No se encontró el contribuyente con cuenta {cuenta}.");
                    return Content(HttpStatusCode.NotFound, respuesta);
                }

                respuesta.Resultado = contribuyente;
                return Ok(respuesta);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                respuesta.AgregarMensajeDeError($"Error de base de datos: {ex.Message}");
                return Content(HttpStatusCode.InternalServerError, respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error inesperado: {ex.Message}");
                return Content(HttpStatusCode.InternalServerError, respuesta);
            }
        }
    }
}
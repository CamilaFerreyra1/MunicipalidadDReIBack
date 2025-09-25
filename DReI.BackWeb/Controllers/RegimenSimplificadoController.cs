using System;
using System.Web.Http;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Responses;
using DReI.BackWeb.Services;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/regimen-simplificado")]
    public class RegimenSimplificadoController : ApiController
    {
        private readonly RegimenSimplificadoService _service;

        public RegimenSimplificadoController(RegimenSimplificadoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("adherir")]
        public IHttpActionResult Adherir([FromBody] AdhesionRequest request)
        {
            var respuesta = new Respuesta();

            try
            {
                var msg = _service.AdherirAlRegimen(request.Cuenta, request.PeriodoDesde, request.Email, request.TE, request.CUIT, request.Vigencia, request.Usr);
                respuesta.Resultado = new { Mensaje = msg };
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }

        [HttpGet]
        [Route("estado/{cuenta}/{periodo}")]
        public IHttpActionResult ObtenerEstado(int cuenta, DateTime periodo)
        {
            var respuesta = new Respuesta();

            try
            {
                var estado = _service.ObtenerEstado(cuenta, periodo);
                respuesta.Resultado = estado;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError($"Error: {ex.Message}");
                return Content(System.Net.HttpStatusCode.InternalServerError, respuesta);
            }
        }
    }

    public class AdhesionRequest
    {
        public int Cuenta { get; set; }
        public DateTime PeriodoDesde { get; set; }
        public string Email { get; set; }
        public string TE { get; set; }
        public string CUIT { get; set; }
        public VigenciaDto Vigencia { get; set; }
        public int Usr { get; set; }
    }
}
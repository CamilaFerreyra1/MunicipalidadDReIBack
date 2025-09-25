using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Dto;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/vigencias")]
    public class VigenciasController : ApiController
    {
        private readonly VigenciasService _service;

        public VigenciasController(VigenciasService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("regimenes")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerRegimenes()
        {
            var regimenes = _service.ObtenerRegimenes();
            return Ok(regimenes);
        }

        [HttpGet]
        [Route("cuenta/{cuenta}/fecha/{fecha}")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerVigencias(int cuenta, DateTime fecha)
        {
            var vigencias = _service.ObtenerVigencias(fecha, cuenta);
            return Ok(vigencias);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerPorId(int id)
        {
            var vigencia = _service.ObtenerPorId(id);
            if (vigencia == null)
                return NotFound();

            return Ok(vigencia);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult Crear(VigenciaDto dto)
        {
            var nuevaVigencia = _service.CrearVigencia(dto, 1); // ⚠️ ID usuario temporal
            return Ok(nuevaVigencia);
        }

        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult Modificar(int id, VigenciaDto dto)
        {
            _service.ModificarVigencia(id, dto, 1); // ⚠️ ID usuario temporal
            return Ok(new { mensaje = "Vigencia modificada correctamente" });
        }

        [HttpDelete]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult Eliminar(int id)
        {
            _service.EliminarVigencia(id, 1); // ⚠️ ID usuario temporal
            return Ok(new { mensaje = "Vigencia eliminada correctamente" });
        }
    }
}

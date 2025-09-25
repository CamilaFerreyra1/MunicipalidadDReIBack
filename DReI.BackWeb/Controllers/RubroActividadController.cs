using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using DReI.BackWeb.Models;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Responses;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/rubros-actividades")]
    public class RubroActividadController : ApiController
    {
        private readonly RubroActividadService _service;

        public RubroActividadController(RubroActividadService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("rubro/{idRubro}")]
        public async Task<IHttpActionResult> GetPorRubro(int idRubro)
        {
            var respuesta = await _service.ObtenerActividadesPorRubroAsync(idRubro);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(respuesta.Resultado);
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IHttpActionResult> Crear([FromBody] RubroActividadCreateDto dto)
        {
            if (!ModelState.IsValid || dto?.CodigosActividades == null)
                return BadRequest("Datos inválidos");

            var respuesta = await _service.CrearAsync(dto);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Actividades asignadas correctamente" });
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IHttpActionResult> Eliminar([FromBody] RubroActividadCreateDto dto)
        {
            if (!ModelState.IsValid || dto?.CodigosActividades == null)
                return BadRequest("Datos inválidos");

            var respuesta = await _service.EliminarAsync(dto);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Actividades eliminadas correctamente" });
        }
    }
}
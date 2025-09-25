using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Responses;
using DReI.BackWeb.Services;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/rubros-contribuyentes")]
    public class RubroContribuyenteController : ApiController
    {
        private readonly RubroContribuyenteService _service;

        public RubroContribuyenteController(RubroContribuyenteService service)
        {
            _service = service;
        }

        // CREAR
        [HttpPost]
        [Route("crear")]
        public async Task<IHttpActionResult> Crear([FromBody] RubroContribuyenteCreateDto dto)
        {
            if (!ModelState.IsValid || dto?.CodigosActividades == null || !dto.CodigosActividades.Any())
                return BadRequest("Datos inválidos");

            var respuesta = await _service.EliminarAsync(dto.IdRubro, dto.CodigosActividades, dto.IdUsuario);

            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Relaciones creadas correctamente" });
        }

        // ELIMINAR LISTA
        [HttpDelete]
        [Route("eliminar")]
        public async Task<IHttpActionResult> Eliminar([FromBody] RubroContribuyenteCreateDto dto)
        {
            if (!ModelState.IsValid || dto?.CodigosActividades == null || !dto.CodigosActividades.Any())
                return BadRequest("Datos inválidos");

            var respuesta = await _service.EliminarAsync(dto.IdRubro, dto.CodigosActividades, dto.IdUsuario);

            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Relaciones eliminadas correctamente" });
        }

        // ELIMINAR ESPECÍFICO
        [HttpDelete]
        [Route("eliminar-especifico")]
        public async Task<IHttpActionResult> EliminarEspecifico(
            [FromUri] int idRubro,
            [FromUri] int codActividad,
            [FromUri] int nroInscripcion,
            [FromUri] int idUsuario)
        {
            var respuesta = await _service.EliminarPorInscripcionAsync(idRubro, codActividad, nroInscripcion, idUsuario);

            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Relación eliminada correctamente" });
        }
    }
}

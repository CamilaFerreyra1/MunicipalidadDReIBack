using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Responses;
using DReI.BackWeb.Services;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/rubros")]
    public class RubroController : ApiController
    {
        private readonly RubroService _service;

        public RubroController(RubroService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var respuesta = await _service.ObtenerTodosAsync();
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(respuesta.Resultado);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var respuesta = await _service.ObtenerPorIdAsync(id);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(respuesta.Resultado);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Crear([FromBody] RubroCreateDto dto)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(dto?.Descripcion) || dto.CodigosActividades == null)
                return BadRequest("Datos inválidos");

            var respuesta = await _service.CrearAsync(dto);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Rubro creado correctamente" });
        }

        [HttpPut]
        public async Task<IHttpActionResult> Modificar([FromBody] RubroUpdateDto dto)
        {
            if (!ModelState.IsValid || dto?.IdRubro <= 0 || string.IsNullOrWhiteSpace(dto.Descripcion) || dto.CodigosActividades == null)
                return BadRequest("Datos inválidos");

            var respuesta = await _service.ModificarAsync(dto);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Rubro modificado correctamente" });
        }

        [HttpDelete]
        [Route("{idRubro}")]
        public async Task<IHttpActionResult> Eliminar(int idRubro, [FromUri] int idUsuario)
        {
            if (idRubro <= 0 || idUsuario <= 0)
                return BadRequest("Parámetros inválidos");

            var respuesta = await _service.EliminarAsync(idRubro, idUsuario);
            if (respuesta.CantidadDeErrores > 0)
            {
                string errores = string.Join("; ", respuesta.Error.Select(e => e?.ToString()));
                return BadRequest(errores);
            }
            return Ok(new { mensaje = "Rubro eliminado correctamente" });
        }
    }
}
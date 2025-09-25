using System;
using System.Threading.Tasks;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Dto;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/monotributo")]
    public class MonotributoController : ApiController
    {
        private readonly MonotributoService _service;

        public MonotributoController(MonotributoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("lista")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ObtenerLista()
        {
            var lista = await _service.ListaAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("cuenta/{cuenta}/periodo/{periodo}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Obtener(int cuenta, DateTime periodo, bool traerDadosDeBaja = false)
        {
            var monotributo = await _service.ObtenerAsync(cuenta, periodo, traerDadosDeBaja);
            if (monotributo == null)
                return NotFound();
            return Ok(monotributo);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Crear(MonotributoDto dto)
        {
            await _service.CrearAsync(dto, 1); // ID de usuario temporal
            return Ok(new { mensaje = "Monotributo creado correctamente" });
        }

        [HttpPut]
        [Route("")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Modificar(MonotributoDto dto)
        {
            await _service.ModificarAsync(dto, 1); // ID de usuario temporal
            return Ok(new { mensaje = "Monotributo modificado correctamente" });
        }

        [HttpDelete]
        [Route("cuenta/{cuenta}/fecha/{fecha}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Eliminar(int cuenta, DateTime fecha)
        {
            await _service.EliminarAsync(cuenta, fecha, 1); // ID de usuario temporal
            return Ok(new { mensaje = "Monotributo eliminado correctamente" });
        }
    }
}
using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Data;
using DReI.BackWeb.Services.Utils;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/vigencias")]
    public class VigenciasController : ApiController
    {
        [HttpGet]
        [Route("regimenes")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerRegimenes()
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var rutinasService = new RutinasService(context); 
                var service = new VigenciasService(context, rutinasService); 
                var regimenes = service.ObtenerRegimenes();
                return Ok(regimenes);
            }
        }

        [HttpGet]
        [Route("cuenta/{cuenta}/fecha/{fecha}")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerVigencias(int cuenta, DateTime fecha)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var rutinasService = new RutinasService(context);
                var service = new VigenciasService(context, rutinasService);
                var vigencias = service.ObtenerVigencias(fecha, cuenta);
                return Ok(vigencias);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerPorId(int id)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var rutinasService = new RutinasService(context);
                var service = new VigenciasService(context, rutinasService);
                var vigencia = service.ObtenerPorId(id);
                if (vigencia == null)
                    return NotFound();
                return Ok(vigencia);
            }
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult Crear(VigenciaDto dto)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var rutinasService = new RutinasService(context);
                var service = new VigenciasService(context, rutinasService);
                var nuevaVigencia = service.CrearVigencia(dto, 1); // ID de usuario temporal
                return Ok(nuevaVigencia);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult Modificar(int id, VigenciaDto dto)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var rutinasService = new RutinasService(context);
                var service = new VigenciasService(context, rutinasService);
                service.ModificarVigencia(id, dto, 1); // ID de usuario temporal
                return Ok(new { mensaje = "Vigencia modificada correctamente" });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult Eliminar(int id)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var rutinasService = new RutinasService(context);
                var service = new VigenciasService(context, rutinasService);
                service.EliminarVigencia(id, 1); // ID de usuario temporal
                return Ok(new { mensaje = "Vigencia eliminada correctamente" });
            }
        }
    }
}
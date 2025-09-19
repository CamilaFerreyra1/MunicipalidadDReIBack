using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Data;
using System.Linq;


namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/configuracion")]
    public class ConfiguracionTributariaController : ApiController
    {
        [HttpGet]
        [Route("lista")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerLista()
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var service = new ConfiguracionTributariaService(context);
                var configuraciones = service.ObtenerLista().ToList();
                return Ok(configuraciones);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerPorId(int id)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var service = new ConfiguracionTributariaService(context);
                var configuracion = service.ObtenerPorId(id);
                if (configuracion == null)
                    return NotFound();
                return Ok(configuracion);
            }
        }
    }
}
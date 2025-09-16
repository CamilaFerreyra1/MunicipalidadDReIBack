using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/configuraciones")]
    public class ConfigTributariaController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(ConfiguracionTributaria config)
        {
            int usuarioActual = ObtenerUsuarioDesdeToken(); 

            var service = new ConfigTributariaService();
            service.CrearConfiguracion(config, usuarioActual);

            return Ok();
        }

        private int ObtenerUsuarioDesdeToken()
        {
            // Ejemplo: si usas JWT, podrías hacer:
            // var identity = User.Identity as ClaimsIdentity;
            // var userIdClaim = identity?.FindFirst("UserId");
            // return int.Parse(userIdClaim?.Value ?? "0");

            // Por ahora, para pruebas:
            return 1;
        }
    }
}
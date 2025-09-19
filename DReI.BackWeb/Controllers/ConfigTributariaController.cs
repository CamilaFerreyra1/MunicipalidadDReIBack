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
            // VER LO DEL TOKEN 
            // Por ahora, para pruebas:
            return 1;
        }
    }
}
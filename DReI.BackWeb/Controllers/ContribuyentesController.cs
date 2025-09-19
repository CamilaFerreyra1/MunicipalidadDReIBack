using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Data;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/contribuyentes")]
    public class ContribuyentesController : ApiController 
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("{cuenta:int}")]
        public IHttpActionResult Obtener(int cuenta, bool traerDadosDeBaja = false)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var service = new ContribuyentesService(context);
                var contribuyente = service.Obtener(cuenta, traerDadosDeBaja);

                if (contribuyente == null)
                    return NotFound();

                return Ok(contribuyente);
            }
        }
    }
}
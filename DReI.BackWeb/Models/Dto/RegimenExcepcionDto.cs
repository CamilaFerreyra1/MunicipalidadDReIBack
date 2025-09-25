using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class RegimenExcepcionDto
    {
        public int IdExcepcion { get; set; }
        public int NRO_INCRIPCION { get; set; }
        public DateTime FHasta { get; set; }
        public bool PermitirAdhesion { get; set; }
        public decimal ValorUCM { get; set; }
        public DateTime FAlta { get; set; }
        public int UsrAlta { get; set; }
        public DateTime? FBaja { get; set; }
        public int? UsrBaja { get; set; }
    }
}
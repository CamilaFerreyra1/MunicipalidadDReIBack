using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class MonotributoDto
    {
        public int NRO_INCRIPCION { get; set; }
        public DateTime FVigenciaDesde { get; set; }
        public DateTime? FVigenciaHasta { get; set; }
        public int IdVigencia { get; set; }
        public int CantUCM { get; set; }
        public decimal ValorUCM { get; set; }
        public string Email { get; set; }
        public string TE { get; set; }
        public DateTime? FVerificado { get; set; }
        public string CUIT { get; set; }
        public string TIPO_RETENCION_DRI { get; set; }
    }
}
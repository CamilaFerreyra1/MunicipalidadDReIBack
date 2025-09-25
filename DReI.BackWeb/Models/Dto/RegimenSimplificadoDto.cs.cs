using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class RegimenSimplificadoDto
    {
        public int NRO_INCRIPCION { get; set; }
        public bool Adherido { get; set; }
        public VigenciaDto CategoriaSeleccionada { get; set; } 
        public DateTime FTopeAdhesion { get; set; }
        public bool TieneDeuda { get; set; }
        public string Mensaje { get; set; }
        public string ComentarioAdmin { get; set; }
        public bool Verificado { get; set; }
        public List<DateTime> PeriodosEnRegimen { get; set; } = new List<DateTime>();
    }

    //public class VigenciaDto
    //{
    //    public int IdVigencia { get; set; }
    //    public string Categoria { get; set; }
    //    public DateTime FVigenciaDesde { get; set; }
    //    public DateTime FVigenciaHasta { get; set; }
    //    public decimal ValorUCM { get; set; }
    //    public int CantUCMs { get; set; }
    //    public decimal PorcDescPC { get; set; }
    //    public DateTime FVto1erPeriodo { get; set; }
    //}
}
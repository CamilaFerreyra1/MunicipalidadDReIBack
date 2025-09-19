using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class VigenciaDto
    {
        public int IdVigencia { get; set; }
        public string Categoria { get; set; }
        public int CantUCMs { get; set; }
        public decimal PorcDescPC { get; set; }
        public DateTime FVigenciaDesde { get; set; }
        public DateTime FVigenciaHasta { get; set; }
        public decimal ValorUCM { get; set; }
        public int CantidadCuotas { get; set; }
    }

    public class VigenciasRegimenDto
    {
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        public List<VigenciaDto> Categorias { get; set; } = new List<VigenciaDto>();
    }
}
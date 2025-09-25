using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Models.Dto
{
    public class EstadoRegimenContribuyente
    {
        public int NRO_INCRIPCION { get; set; }
        public bool Adherido { get; set; }
        public Vigencias CategoriaSeleccionada { get; set; } // tabla DRI_CatMonoVigencias
        public DateTime FTopeAdhesion { get; set; }
        public bool TieneDeuda { get; set; }
        public string Mensaje { get; set; }
        public string ComentarioAdmin { get; set; }
        public bool Verificado { get; set; }
        public List<DateTime> PeriodosEnRegimen { get; set; }
    }
}
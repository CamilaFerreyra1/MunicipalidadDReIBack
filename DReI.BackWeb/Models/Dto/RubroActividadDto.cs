using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class RubroActividadDto
    {
        public int Id { get; set; }
        public int IdRubro { get; set; }
        public int CodActividad { get; set; }
        public int Comuna { get; set; }
        public int NroNomenclador { get; set; }
        public DateTime FAlta { get; set; }
        public int UsrAlta { get; set; }
        // Puedes excluir FBaja si solo quieres activos
    }

    public class RubroActividadCreateDto
    {
        public int IdRubro { get; set; }
        public List<int> CodigosActividades { get; set; } = new List<int>();
        public int IdUsuario { get; set; }
    }
}
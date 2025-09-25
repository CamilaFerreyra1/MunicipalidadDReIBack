using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class RubroDto
    {
        public int IdRubro { get; set; }
        public string Descripcion { get; set; }
    }

    public class RubroCreateDto
    {
        public string Descripcion { get; set; }
        public List<int> CodigosActividades { get; set; } = new List<int>();
        public int IdUsuario { get; set; }
    }

    public class RubroUpdateDto
    {
        public int IdRubro { get; set; }
        public string Descripcion { get; set; }
        public List<int> CodigosActividades { get; set; } = new List<int>();
        public int IdUsuario { get; set; }
    }

}
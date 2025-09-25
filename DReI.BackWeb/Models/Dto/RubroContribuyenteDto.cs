using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Models.Dto
{
    public class RubroContribuyenteCreateDto
    {
        public int IdRubro { get; set; }
        public List<int> CodigosActividades { get; set; }
        public int IdUsuario { get; set; }
    }
}
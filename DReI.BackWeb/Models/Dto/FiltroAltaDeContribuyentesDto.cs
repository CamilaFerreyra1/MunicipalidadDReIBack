using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class FiltroAltaDeContribuyentesDto
	{
		//0 = Ambos
		//1 = Inicio actividades
		//2 = Clausura
		public int TipoBusqueda { get; set; }

		public DateTime FDesde { get; set; }

		public DateTime FHasta { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
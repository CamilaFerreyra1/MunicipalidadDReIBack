using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
	public class ReporteAltaDeContribuyentesDTO
	{
		public List<M03DRI> Contribuyentes { get; set; } = new List<M03DRI>();

		public int CantidadDeIniciosDeActividades { get; set; } = 0;

		public int CantidadDeClausuras { get; set; } = 0;

		public int CantidadDeProcesosDeBajas { get; set; } = 0;

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class ConsultarDeudaDto
	{
		public int Comuna { get; set; } = 0;

		public int Sistema { get; set; } = 0;

		public int SubcodigoSistema { get; set; } = 0;

		public List<int> NrosDeCuenta { get; set; } = new List<int>();

		public int NroSubcuenta { get; set; } = 0;

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
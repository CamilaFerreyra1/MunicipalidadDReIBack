using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class ResumenDeDeudaDto
	{
		public int NroCuenta { get; set; }

		public ResumenDeDeudaDto()
		{
			PeriodosAdeudados = new List<N00CTACT>();
			ConveniosAdeudados = new List<ResumenDeDeuda_ConveniosDto>();
		}

		public List<N00CTACT> PeriodosAdeudados { get; set; }

		public List<ResumenDeDeuda_ConveniosDto> ConveniosAdeudados { get; set; }

		public string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
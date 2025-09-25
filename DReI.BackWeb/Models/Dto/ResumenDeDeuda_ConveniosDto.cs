using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    public class ResumenDeDeuda_ConveniosDto
	{
		public ResumenDeDeuda_ConveniosDto()
		{
			PeriodosAdeudados = new List<N00CTACT>();
		}

		public int NroConvenio { get; set; } = 0;

		public List<N00CTACT> PeriodosAdeudados { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
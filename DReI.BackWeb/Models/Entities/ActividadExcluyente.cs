using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_RegSimplif_ActividadesExcluyentes")] 
    public class ActividadExcluyente
    {
        [Key]
        public int IdReg { get; set; }

        [Required]
        public int CodActividad { get; set; }

        [Required]
        public int NroNomenclador { get; set; }

        [Required]
        public DateTime FVigenciaDesde { get; set; }

        [Required]
        public DateTime FVigenciaHasta { get; set; }

    }
}
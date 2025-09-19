using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_RegSimplif_ContribConDeuda")]
    public class ContribuyenteConDeuda
    {
        [Key]
        public int IdReg { get; set; }

        [Required]
        public int NRO_INCRIPCION { get; set; }

        [Required]
        public DateTime PeridoHasta { get; set; } // no esta mal escrito

        [Required]
        public DateTime FUltVerificacionConDeuda { get; set; }

        [Required]
        public DateTime FBaja { get; set; }

    }
}
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
        public DateTime PeridoHasta { get; set; } // tal vez sea "PeriodoHasta"? (revisa ortografía en DB)

        [Required]
        public DateTime FUltVerificacionConDeuda { get; set; }

        [Required]
        public DateTime FBaja { get; set; }

        // 🔗 Propiedad de navegación (opcional, descomenta si la necesitas)

        // [ForeignKey("NRO_INCRIPCION")]
        // public virtual ContribuyenteDetalle Contribuyente { get; set; }
    }
}
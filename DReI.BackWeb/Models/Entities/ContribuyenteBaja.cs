using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_CatMonoContrib_BAJAS")]
    public class ContribuyenteBaja
    {
        [Key]
        public int IdLog { get; set; }

        [Required]
        public int NRO_INCRIPCION { get; set; }

        [Required]
        public DateTime FVigenciaDesde { get; set; }

        [Required]
        public DateTime FVigenciaHasta { get; set; }

        [Required]
        public int IdVigencia { get; set; }

        [Required]
        public int CantUCM { get; set; }

        [Required]
        public decimal ValorUCM { get; set; }

        [Required]
        public int UsrAlta { get; set; }

        [Required]
        public DateTime FAlta { get; set; }

        [Required]
        public int UsrModi { get; set; }

        [Required]
        public DateTime FModi { get; set; }

        [Required]
        public int UsrBaja { get; set; }

        [Required]
        public DateTime FBaja { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50)]
        public string TE { get; set; }

        [Required]
        public DateTime FVerificado { get; set; }

        [StringLength(13)]
        public string CUIT { get; set; }

        [Required]
        public int TIPO_RETENCION_DRI { get; set; }

        // 🔗 Propiedad de navegación (opcional, descomenta si la necesitas)

        // [ForeignKey("NRO_INCRIPCION")]
        // public virtual ContribuyenteDetalle Contribuyente { get; set; }
    }
}
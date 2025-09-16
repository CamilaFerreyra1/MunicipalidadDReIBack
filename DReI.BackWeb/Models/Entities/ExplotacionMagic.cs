using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("M03EXPLO1_orig")]
    public class ExplotacionMagic
    {
        [Key, Column(Order = 1)]
        public short COMUNA { get; set; }

        [Key, Column(Order = 2)]
        public int COD_EXPLOTACION { get; set; }

        [Required]
        [StringLength(300)]
        public string DENO_EXPLOTACION { get; set; }

        [Required]
        public short COD_PAGO_SEG_EXPL { get; set; }

        [Required]
        public short MOTIVO_BAJA { get; set; }

        [Required]
        public DateTime FECHA_BAJA { get; set; }

        [StringLength(50)]
        public string USUARIO_BAJA { get; set; }

        [Required]
        public DateTime HORA_ALTA { get; set; }

        [Required]
        public DateTime FECHA_ALTA { get; set; }

        [StringLength(50)]
        public string USUARIO_ALTA { get; set; }

        [Required]
        public DateTime HORA_MODIF { get; set; }

        [Required]
        public DateTime FECHA_MODIF { get; set; }

        [StringLength(50)]
        public string USUARIO_MODIF { get; set; }

        [Required]
        public int NroNomenclador { get; set; }
    }
}
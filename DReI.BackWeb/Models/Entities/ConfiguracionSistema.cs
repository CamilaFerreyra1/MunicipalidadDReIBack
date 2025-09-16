using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("SIS_Configuracion")]
    public class ConfiguracionSistema
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public decimal ValorDec { get; set; }

        [Required]
        public int ValorInt { get; set; }

        [Required]
        public DateTime ValorDT { get; set; }

        [StringLength(500)]
        public string ValorStr { get; set; }

        [Required]
        public DateTime FVigenciaDesde { get; set; }

        [Required]
        public DateTime FVigenciaHasta { get; set; }
    }
}
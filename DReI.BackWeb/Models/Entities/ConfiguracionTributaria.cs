using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [MetadataType(typeof(ConfiguracionTributariaMetadata))]
    [Table("DRI_Configuracion")]
    public class ConfiguracionTributaria
    {
        public ConfiguracionTributaria()
        {      
        }

        public void ValoresPorDefecto(int usuarioAlta)
        {
            UsrAlta = usuarioAlta;
            FAlta = DateTime.Now;
            UsrBaja = 0;
            FBaja = new DateTime(1900, 1, 1);
        }

        [Key]
        public int IdConfig { get; set; }

        [Required]
        public int IdTipoCalc { get; set; }

        [Required]
        public DateTime FDesde { get; set; }

        [Required]
        public DateTime FHasta { get; set; }

        [Required]
        public decimal Alicuota { get; set; }

        [Required]
        public decimal CantUCM { get; set; }

        [JsonIgnore]
        public DateTime FAlta { get; set; }

        [JsonIgnore]
        public int UsrAlta { get; set; }

        [JsonIgnore]
        public DateTime FBaja { get; set; }

        [JsonIgnore]
        public int UsrBaja { get; set; }

        [JsonIgnore]
        public virtual ICollection<ConfiguracionActividad> Actividades { get; set; } 
    }

    public class ConfiguracionTributariaMetadata
    {
        [Required]
        public int IdTipoCalc { get; set; }

        [Required]
        public DateTime FDesde { get; set; }

        [Required]
        public DateTime FHasta { get; set; }

        [Required]
        public decimal Alicuota { get; set; }

        [Required]
        public decimal CantUCM { get; set; }
    }
}
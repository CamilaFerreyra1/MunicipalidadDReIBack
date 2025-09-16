using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DReI.BackWeb.Models.Entities
{
    [MetadataType(typeof(ConfiguracionActividadMetadata))]
    [Table("DRI_ConfiguracionActividades")]
    public class ConfiguracionActividad
    {
        public ConfiguracionActividad()
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
        public int IdRegConfig { get; set; }

        [Required]
        public int IdConfig { get; set; }

        [Required]
        public int NroNomenclador { get; set; }

        [Required]
        public int CodActividad { get; set; }

        [JsonIgnore]
        public int UsrAlta { get; set; }

        [JsonIgnore]
        public DateTime FAlta { get; set; }

        [JsonIgnore]
        public int UsrBaja { get; set; }

        [JsonIgnore]
        public DateTime FBaja { get; set; }

        [ForeignKey("IdConfig")]
        public virtual ConfiguracionTributaria Configuracion { get; set; }
    }

    public class ConfiguracionActividadMetadata 
    {
        [Required]
        public int IdConfig { get; set; }

        [Required]
        public int NroNomenclador { get; set; }

        [Required]
        public int CodActividad { get; set; }
    }
}
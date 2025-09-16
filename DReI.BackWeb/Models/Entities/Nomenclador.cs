using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_Nomencladores")] 
    public class Nomenclador
    {
        [Key]
        public int NroNomenclador { get; set; }

        [Required]
        public DateTime VigenciaHasta { get; set; }

        public string Comentario { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_RegimenExepciones")] 
    [MetadataType(typeof(RegimenExcepcionMetadata))]
    public class RegimenExcepcion
    {
        public RegimenExcepcion()
        {
        }

        public void ValoresPorDefecto()
        {
            FAlta = DateTime.Now;
            UsrAlta = 0;
            FBaja = new DateTime(1900, 1, 1);
            UsrBaja = 0;
        }

        [NotMapped] 
        public bool Vigente
        {
            get => FHasta >= DateTime.Today;
        }


        [Key]
        public int IdExcepcion { get; set; }

        [Required]
        public int NRO_INCRIPCION { get; set; }

        [Required]
        public bool PermitirAdhesion { get; set; }
        public decimal ValorUCM { get; set; }

        [Required]
        public DateTime FHasta { get; set; }
        public DateTime FAlta { get; set; }
        public int UsrAlta { get; set; }
        public DateTime FBaja { get; set; }
        public int UsrBaja { get; set; }

    }

    public class RegimenExcepcionMetadata
    { 
    }
}
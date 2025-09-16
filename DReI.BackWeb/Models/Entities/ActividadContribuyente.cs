using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_ActividadesContrib")] 
    public class ActividadContribuyente
    {
        public ActividadContribuyente()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int COMUNA { get; set; }

        [Required]
        public int NRO_INCRIPCION { get; set; }

        [Required]
        public int CodActividad { get; set; }

        [Required]
        public int NroNomenclador { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        public DateTime FInicio { get; set; }

        [Required]
        public DateTime FAlta { get; set; }

        [Required]
        public int UsrAlta { get; set; }

        public DateTime? FModi { get; set; }
        public int? UsrModi { get; set; }
        public DateTime? FBaja { get; set; }
        public int? UsrBaja { get; set; }

        [Required]
        public DateTime FFin { get; set; }

        [Required]
        public int CodBaja { get; set; }

        public string Observaciones { get; set; }

        // 🔗 Propiedades de navegación (opcional, descomenta si las necesitas)

        // [ForeignKey("NRO_INCRIPCION")]
        // public virtual ContribuyenteDetalle Contribuyente { get; set; }

        // Si tienes una entidad "Actividad", puedes agregar:
        // [ForeignKey("CodActividad")]
        // public virtual Actividad Actividad { get; set; }
    }
}
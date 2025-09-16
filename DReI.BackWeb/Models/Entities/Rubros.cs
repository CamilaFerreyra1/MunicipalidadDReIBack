using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_Rubros")] 
    public class Rubros
    {
        public Rubros()
        {
        }

        public void ValoresPorDefecto()
        {
            FAlta = DateTime.Now;
            UsrAlta = 0;
            FModi = new DateTime(1900, 1, 1);
            UsrModi = 0;
            FBaja = new DateTime(1900, 1, 1);
            UsrBaja = 0;
        }

        [Key]
        public int IdRubro { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        public bool Mostrar { get; set; } 

        [Required]
        public DateTime FAlta { get; set; }

        [Required]
        public int UsrAlta { get; set; }

        [Required]
        public DateTime FModi { get; set; }

        [Required]
        public int UsrModi { get; set; }

        [Required]
        public DateTime FBaja { get; set; }

        [Required]
        public int UsrBaja { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_CatMonoVigencias")]
       [MetadataType(typeof(VigenciaMetadata))]
    public class Vigencias
    {
        public Vigencias()
        {
            CantidadCuotas = 6; // ver si lo dejamos en 6 o en 12
        }

        [Key]
        public int IdVigencia { get; set; }

        [Required]
        [StringLength(100)]
        public string Categoria { get; set; }

        [Required]
        public int CantUCMs { get; set; }

        [Required]
        public decimal PorcDescPC { get; set; }

        [Required]
        public DateTime FVigenciaDesde { get; set; }

        [Required]
        public DateTime FVigenciaHasta { get; set; }

        public int UsrAlta { get; set; }
        public DateTime FAlta { get; set; }
        public int UsrModi { get; set; }
        public DateTime FModi { get; set; }
        public int UsrBaja { get; set; }
        public DateTime FBaja { get; set; }

        private decimal _valorUCM;
        public decimal ValorUCM
        {
            get => _valorUCM;
            set => _valorUCM = value;
        }

        public int CantidadCuotas { get; set; } = 12;

        public void ValoresPorDefecto()
        {
            FAlta = DateTime.Now;
            UsrAlta = 0;
            FModi = new DateTime(1900, 1, 1);
            UsrModi = 0;
            FBaja = new DateTime(1900, 1, 1);
            UsrBaja = 0;
        }
    }

    public class VigenciaMetadata
    {
        [Required]
        public int IdVigencia { get; set; }

        [Required]
        [StringLength(100)]
        public string Categoria { get; set; }

        [Required]
        public int CantUCMs { get; set; }

        [Required]
        public decimal PorcDescPC { get; set; }

        [Required]
        public DateTime FVigenciaDesde { get; set; }

        [Required]
        public DateTime FVigenciaHasta { get; set; }

        public int UsrAlta { get; set; }
        public DateTime FAlta { get; set; }
        public int UsrModi { get; set; }
        public DateTime FModi { get; set; }
        public int UsrBaja { get; set; }
        public DateTime FBaja { get; set; }
    }
}
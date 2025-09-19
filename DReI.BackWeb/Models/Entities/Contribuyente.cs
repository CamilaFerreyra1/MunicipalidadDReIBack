using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_CatMonoContrib")]
    public class Contribuyente
    {
        [Key]
        [Column(Order = 0)] 
        public int NRO_INCRIPCION { get; set; }

        [Key]
        [Column(Order = 1)] 
        public DateTime FVigenciaDesde { get; set; }

        public DateTime FVigenciaHasta { get; set; }
        public int IdVigencia { get; set; }
        public int CantUCM { get; set; }
        public decimal ValorUCM { get; set; }
        public int UsrAlta { get; set; }
        public DateTime FAlta { get; set; }
        public int UsrModi { get; set; }
        public DateTime FModi { get; set; }
        public int UsrBaja { get; set; }
        public DateTime FBaja { get; set; }
        public string Email { get; set; }
        public string TE { get; set; }
        public DateTime FVerificado { get; set; }
        public string CUIT { get; set; }
        public string TIPO_RETENCION { get; set; }
        public string MotivoBaja { get; set; }
        public string Comentario { get; set; }

        public virtual ContribuyenteDetalleMagic MAGIC_M03DRI { get; set; }

        public void ValoresPorDefecto()
        {
            FAlta = DateTime.Now;
            UsrAlta = 0;
            FModi = new DateTime(1900, 1, 1);
            UsrModi = 0;
            FBaja = new DateTime(1900, 1, 1);
            UsrBaja = 0;
            FVerificado = new DateTime(1900, 1, 1);
            Email = string.Empty;
            TE = string.Empty;
            CUIT = string.Empty;
        }
    }
}

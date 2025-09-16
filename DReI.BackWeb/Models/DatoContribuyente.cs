using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI__DatosContrib")]
    public class DatoContribuyente
    {
        [Key]
        public int IdSolicitud { get; set; }

        public int? COMUNA { get; set; }
        public int? NRO_INCRIPCION { get; set; }

        [StringLength(300)]
        public string RazonSocial { get; set; }

        [StringLength(13)]
        public string CUIT { get; set; }

        [StringLength(50)]
        public string IngBrutos { get; set; }

        [StringLength(10)]
        public string F_CodProvincia { get; set; }

        [StringLength(10)]
        public string F_CodLocalidad { get; set; }

        [StringLength(200)]
        public string F_OtraLocalidad { get; set; }

        [StringLength(10)]
        public string F_CodCalle { get; set; }

        [StringLength(200)]
        public string F_OtraCalle { get; set; }

        public int? F_Puerta { get; set; }
        public int? F_Piso { get; set; }

        [StringLength(10)]
        public string F_Dpto { get; set; }

        // 🔹 Datos de Comercial (C_)
        [StringLength(10)]
        public string C_CodProvincia { get; set; }

        [StringLength(10)]
        public string C_CodLocalidad { get; set; }

        [StringLength(200)]
        public string C_OtraLocalidad { get; set; }

        [StringLength(10)]
        public string C_CodCalle { get; set; }

        [StringLength(200)]
        public string C_OtraCalle { get; set; }

        public int? C_Puerta { get; set; }
        public int? C_Piso { get; set; }

        [StringLength(10)]
        public string C_Dpto { get; set; }

        [StringLength(50)]
        public string TE { get; set; }

        [StringLength(255)]
        [EmailAddress]
        public string EMail { get; set; }

        [StringLength(50)]
        public string Catastro { get; set; }

        public bool? EsSociedad { get; set; }
        public int? TipoSociedad { get; set; }

        public string DatosSocios { get; set; } 

        [Required]
        public int Estado { get; set; }

        public string Comentario { get; set; }

        [Required]
        public DateTime FECHA_PROC_MAGIC { get; set; }

        [Required]
        public DateTime HORA_PROC_MAGIC { get; set; }

        [StringLength(50)]
        public string USU_PROC_MAGIC { get; set; }

        [Required]
        public DateTime FAlta { get; set; }

        [Required]
        public int UsrAlta { get; set; }

        public DateTime? FModi { get; set; }
        public int? UsrModi { get; set; }
        public DateTime? FBaja { get; set; }
        public int? UsrBaja { get; set; }
    }
}
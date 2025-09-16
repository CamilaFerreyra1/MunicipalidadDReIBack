using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("MAGIC_C03CARAA")] 
    public class CaratulaMagic
    {
        [Key, Column(Order = 1)]
        public short COMUNA_CAR { get; set; }

        [Key, Column(Order = 2)]
        public DateTime PERIODO_CAR { get; set; }

        [Required]
        public DateTime F_VTO_CAR { get; set; }

        [Required]
        public DateTime FVTO_API0_CAR { get; set; }

        [Required]
        public DateTime FVTO_API1_CAR { get; set; }

        [Required]
        public DateTime FVTO_API2_CAR { get; set; }

        [Required]
        public DateTime FVTO_API3_CAR { get; set; }

        [Required]
        public DateTime FVTO_API4_CAR { get; set; }

        [Required]
        public DateTime FVTO_API5_CAR { get; set; }

        [Required]
        public DateTime FVTO_API6_CAR { get; set; }

        [Required]
        public DateTime FVTO_API7_CAR { get; set; }

        [Required]
        public DateTime FVTO_API8_CAR { get; set; }

        [Required]
        public DateTime FVTO_API9_CAR { get; set; }

        [Required]
        public double MINIMO_INDUS_CAR { get; set; }

        [Required]
        public double MINIMO_COM_CAR { get; set; }

        [Required]
        public double MINIMO_SERV_CAR { get; set; }

        [Required]
        public double IMPORTE_LIBRE1 { get; set; }

        [Required]
        public double IMPORTE_LIBRE2 { get; set; }

        public string ULTIMA_CUOTA_CAR { get; set; }

        public byte[] MENSAJE_CAR { get; set; }

        [Required]
        public double EMITIDONOMINAL { get; set; }

        [Required]
        public double RECAUD_NOMINAL { get; set; }

        [Required]
        public double RECAUD_NETO { get; set; }

        [Required]
        public double RECARGOS { get; set; }

        [Required]
        public double DEDUCCIONES { get; set; }

        [Required]
        public double EMITIDO_ANUL99 { get; set; }

        [Required]
        public double LIBRE_1 { get; set; }

        [Required]
        public double LIBRE_2 { get; set; }

        [Required]
        public int BOLETAS_EMITIDAS { get; set; }

        [Required]
        public int BOLETAS_CANCEL { get; set; }

        [Required]
        public int BOLETAS_ANUL99 { get; set; }

        [Required]
        public int BOL_LIBRE_1 { get; set; }

        [Required]
        public int BOL_LIBRE_2 { get; set; }

        [Required]
        public short MOTIVO_BAJA_CAR { get; set; }

        [Required]
        public DateTime F_BAJA_CAR { get; set; }

        public string USUA_BAJA_CAR { get; set; }

        [Required]
        public DateTime HORA_ALTA_CAR { get; set; }

        [Required]
        public DateTime F_ALTA_CAR { get; set; }

        public string USUA_ALTA_CAR { get; set; }

        [Required]
        public DateTime HORA_MODIF_CAR { get; set; }

        [Required]
        public DateTime F_MODIF_CAR { get; set; }

        public string USUA_MODIF_CAR { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("MAGIC_M03DRI")]
    public class ContribuyenteDetalleMagic
    {
        public ContribuyenteDetalleMagic()
        {
            Declaraciones = new HashSet<DDJJCabecera>();
        }

        [Key]
        public int NRO_INCRIPCION { get; set; }

        public short COMUNA { get; set; }
        public int CODEXPL_PRIMARIA { get; set; }
        public int CODEXPL_SECUN1 { get; set; }
        public int CODEXPL_SECUN2 { get; set; }
        public int CODEXPL_SECUN3 { get; set; }
        public int CODEXPL_SECUN4 { get; set; }
        public int CODEXPL_SECUN5 { get; set; }
        public string RAZONSOCIAL { get; set; }
        public short TIPO_DOC { get; set; }
        public int NRO_DOC { get; set; }
        public string POSTAL_PARTIC { get; set; }
        public string CALLE_PARTIC { get; set; }
        public int PUERTA_PARTIC { get; set; }
        public string BARRIO_PARTIC { get; set; }
        public byte PISO_PARTIC { get; set; }
        public string DPTO_PARTIC { get; set; }
        public double TELEFONO_PARTIC { get; set; }
        public string POSTAL_COMERC { get; set; }
        public string CALLE_COMERC { get; set; }
        public int PUERTA_COMERC { get; set; }
        public string BARRIO_COMERC { get; set; }
        public byte PISO_COMERC { get; set; }
        public string DPTO_COMERC { get; set; }
        public double TELEFONO_COMERC { get; set; }
        public short CODNAT_JURIDICA { get; set; }
        public short CODCARAC_ESTABLEC { get; set; }
        public string CONV_MULTILATERAL { get; set; }
        public byte CODEXPL_PAGO { get; set; }
        public short ESTUDIO_CONTABLE { get; set; }
        public double NRO_INCRIP_API { get; set; }
        public string NRO_CUIT_DRI { get; set; }
        public DateTime FEC_INICIO { get; set; }
        public DateTime FEC_CLAUSURA { get; set; }
        public DateTime FEC_RECEPCION { get; set; }
        public string EXP_LETRA { get; set; }
        public int EXP_NRO { get; set; }
        public byte EXP_DV { get; set; }
        public short EXP_FICHERO { get; set; }
        public byte EXP_TOMO { get; set; }
        public bool? MARCA_ALTAA { get; set; }
        public int CUENTA_TRIBUTA { get; set; }
        public short CODESTADO_DRI { get; set; }
        public byte CODESTADO_FISCAL { get; set; }
        public short AÑO_ANT_DECLJUR { get; set; }
        public short AÑO_ACT_DECLJUR { get; set; }
        public bool OCUP_DOMIN { get; set; }
        public string CODIGO_1 { get; set; }
        public string CODIGO_2 { get; set; }
        public short MOTIVOBAJA { get; set; }
        public DateTime FECHABAJA { get; set; }
        public string USUARIOBAJA { get; set; }
        public DateTime HORAALTA { get; set; }
        public DateTime FECHAALTA { get; set; }
        public string USUARIOALTA { get; set; }
        public DateTime HORAMODIF { get; set; }
        public DateTime FECHAMODIF { get; set; }
        public string USUARIOMODIF { get; set; }
        public string POSTAL_REAL { get; set; }
        public string CALLE_REAL { get; set; }
        public int PUERTA_REAL { get; set; }
        public string BARRIO_REAL { get; set; }
        public byte PISO_REAL { get; set; }
        public string DPTO_REAL { get; set; }
        public double TELEFONO_REAL { get; set; }
        public DateTime FECHA_AUTORIZA { get; set; }
        public short CANT_UNIDAD { get; set; }
        public string INSCRIP_OFICIO { get; set; }
        public short CASO_SOCIAL { get; set; }
        public string NOM_FANTASIA { get; set; }
        public int CODEXPL_PRIMARIA_MUN { get; set; }
        public int CODEXPL_SECUN1_MUN { get; set; }
        public int CODEXPL_SECUN2_MUN { get; set; }
        public int CODEXPL_SECUN3_MUN { get; set; }
        public int CODEXPL_SECUN4_MUN { get; set; }
        public int CODEXPL_SECUN5_MUN { get; set; }
        public string APELLIDO_CASADA { get; set; }
        public string EMAIL { get; set; }
        public DateTime FECHAHABIWEB { get; set; }
        public DateTime UltPerJuicio { get; set; }
        public int Catastro { get; set; }
        public int SubCatastro { get; set; }
        public int EfectoresSociales { get; set; }
        public DateTime FRECEPCIONTRAMITE { get; set; }
        public DateTime FAPROBACIONTRAMITE { get; set; }


        public virtual ICollection<DDJJCabecera> Declaraciones { get; set; }
    }
}
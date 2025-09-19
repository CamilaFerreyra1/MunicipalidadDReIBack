using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [Table("DRI_DJ_REN")]
    //[MetadataType(typeof(DDJJRenglonMetadata))]
    public class DDJJRenglon
    {
        public DDJJRenglon()
        {
        }

        public void ValoresPorDefecto()
        {
            CodActividad = 0;
            Descripcion = string.Empty;
            IdTipoCal = 0;
            IniActividades = false;
            TotalVentasPais = 0;
            CoeficienteConvMult = 0;
            MontoImponible = 0;
            Alicuota = 0;
            Cantidad = 0;
            ImporteUnitario = 0;
            MontoFijoMensual = 0;
            Ajuste = 0;
            FAlta = DateTime.Now;
            UsrAlta = 0;
            FBaja = null;
            UsrBaja = null;
        }

        public DDJJRenglon Clonar()
        {
            return new DDJJRenglon
            {
                IdRen = 0, // ← Reset PK para nuevo registro
                IdDj = IdDj,
                CodActividad = CodActividad,
                Descripcion = Descripcion,
                IdTipoCal = IdTipoCal,
                IniActividades = IniActividades,
                TotalVentasPais = TotalVentasPais,
                CoeficienteConvMult = CoeficienteConvMult,
                MontoImponible = MontoImponible,
                Alicuota = Alicuota,
                Cantidad = Cantidad,
                ImporteUnitario = ImporteUnitario,
                MontoFijoMensual = MontoFijoMensual,
                Ajuste = Ajuste,
                UsrAlta = UsrAlta,
                FAlta = FAlta,
                UsrBaja = UsrBaja,
                FBaja = FBaja
            };
        }

        [Key]
        public int IdRen { get; set; }

        [Required]
        public int IdDj { get; set; }

        public int CodActividad { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoCal { get; set; }
        public bool IniActividades { get; set; }
        public decimal TotalVentasPais { get; set; }
        public decimal CoeficienteConvMult { get; set; }
        public decimal MontoImponible { get; set; }
        public decimal Alicuota { get; set; }
        public int Cantidad { get; set; }
        public decimal ImporteUnitario { get; set; }
        public decimal MontoFijoMensual { get; set; }
        public decimal Ajuste { get; set; }
        public int UsrAlta { get; set; }
        public DateTime FAlta { get; set; }
        public int? UsrBaja { get; set; }
        public DateTime? FBaja { get; set; }

        [ForeignKey("IdDj")]
        public virtual DDJJCabecera Declaracion { get; set; }
    }

    //public class DDJJRenglonMetadata
    //{
 
    //}
}
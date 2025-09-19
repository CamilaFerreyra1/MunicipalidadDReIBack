using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models
{
    public class DeclaracionesDTO
    { /// <summary>
    ///  ver bien que Campos de usar 
    /// </summary>
        public int IdDJ { get; set; }
        public int PeriodoMes { get; set; }
        public int PeriodoAnio { get; set; }
        public int Secuencia { get; set; }
        public bool RectEspecial { get; set; }
        public decimal DerOcDomPub { get; set; }
        public decimal TotalVentasPais { get; set; }
        public decimal CoeficienteConvMult { get; set; }
        public decimal OtrosPagos { get; set; }
        public decimal SaldoFavor { get; set; }
        public int? PeriodoMesFavor { get; set; }
        public int? PeriodoAnioFavor { get; set; }
        public int NRO_INCRIPCION { get; set; }
        public string Obs { get; set; }
        public bool Finalizado { get; set; }
        public decimal? ImpCuota { get; set; }
        public decimal? ImpRecargo { get; set; }
        public decimal? ImpDeduccion { get; set; }
        public decimal? MontoImponible { get; set; }
        public int? NroLiquidacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaPresentacion { get; set; }
        public bool ProcesadoMagic { get; set; }
        public bool Corregido { get; set; }
        public bool PM { get; set; }
        public int NroNomenclador { get; set; }
        public string IP { get; set; }
        public DateTime FVtoOriginal { get; set; }
        public int IdDjOriginal { get; set; }
        public string TipoLiquidacion { get; set; }
        public string MotivoBaja { get; set; }
        public int Anulada { get; set; }
        public string DJMotos { get; set; }
        public DateTime FPago { get; set; }
        public int CajaPago { get; set; }
        public decimal CreditoFuturo { get; set; }
        public int NroCuotaOrden { get; set; }
        public int NroConvenio { get; set; }
        public int NroCuotaOrdenAjuste { get; set; }
        public decimal ImpBoletaAnt { get; set; }
        public decimal ImpRecargoAnt { get; set; }
        public decimal TotalCreditoTomado { get; set; }
    }
}
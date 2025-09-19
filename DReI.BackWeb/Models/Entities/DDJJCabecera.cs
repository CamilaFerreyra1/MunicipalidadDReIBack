using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DReI.BackWeb.Models.Entities
{
    [MetadataType(typeof(DDJJCabeceraMetadata))]
    public class DDJJCabecera
    {
        public DDJJCabecera()
        {
        }

        [NotMapped] 
        public DateTime Periodo
        {
            get => new DateTime(PeriodoAnio, PeriodoMes, 1);
            set
            {
                PeriodoMes = value.Month;
                PeriodoAnio = value.Year;
            }
        }

        public void ValoresPorDefecto()
        {
            Secuencia = 0;
            RectEspecial = false;
            DerOcDomPub = 0;
            TotalVentasPais = 0;
            CoeficienteConvMult = 0;
            OtrosPagos = 0;
            SaldoFavor = 0;
            PeriodoMesFavor = 0;
            PeriodoAnioFavor = 0;
            Obs = "";
            Finalizado = false;
            ImpCuota = 0;
            ImpRecargo = 0;
            ImpDeduccion = 0;
            MontoImponible = 0;
            NroLiquidacion = 0;
            ProcesadoMagic = false;
            Corregido = false;
            PM = false;
            FechaPresentacion = null;
            UsrPresentacion = null;
            NroNomenclador = 0;
            IP = "";
            IdDjOriginal = 0;
            MotivoBaja = "";
            Anulada = 0;
            DJMotos = "";
            FPago = new DateTime(1900, 1, 1);
            CajaPago = 0;
            CreditoFuturo = 0;
            NroCuotaOrden = 0;
            NroConvenio = 0;
            NroCuotaOrdenAjuste = 0;
            ImpBoletaAnt = 0;
            ImpRecargoAnt = 0;
            TotalCreditoTomado = 0;
            FAlta = DateTime.Now;
            UsrAlta = 0;
            FModi = new DateTime(1900, 1, 1);
            UsrModi = 0;
            FBaja = null;
            UsrBaja = null;
        }

        public DDJJCabecera Clonar()
        {
            return new DDJJCabecera
            {
                PeriodoMes = PeriodoMes,
                PeriodoAnio = PeriodoAnio,
                Secuencia = Secuencia,
                RectEspecial = RectEspecial,
                DerOcDomPub = DerOcDomPub,
                TotalVentasPais = TotalVentasPais,
                CoeficienteConvMult = CoeficienteConvMult,
                OtrosPagos = OtrosPagos,
                SaldoFavor = SaldoFavor,
                PeriodoMesFavor = PeriodoMesFavor,
                PeriodoAnioFavor = PeriodoAnioFavor,
                NRO_INCRIPCION = NRO_INCRIPCION,
                IdUsuario = IdUsuario,
                Obs = Obs,
                Finalizado = Finalizado,
                ImpCuota = ImpCuota,
                ImpRecargo = ImpRecargo,
                ImpDeduccion = ImpDeduccion,
                MontoImponible = MontoImponible,
                NroLiquidacion = NroLiquidacion,
                FechaVencimiento = FechaVencimiento,
                ProcesadoMagic = ProcesadoMagic,
                Corregido = Corregido,
                PM = PM,
                FechaPresentacion = FechaPresentacion,
                UsrPresentacion = UsrPresentacion,
                UsrAlta = UsrAlta,
                FAlta = FAlta,
                UsrModi = UsrModi,
                FModi = FModi,
                UsrBaja = UsrBaja,
                FBaja = FBaja,
                NroNomenclador = NroNomenclador,
                IP = IP,
                FVtoOriginal = FVtoOriginal,
                IdDjOriginal = IdDjOriginal,
                TipoLiquidacion = TipoLiquidacion,
                MotivoBaja = MotivoBaja,
                Anulada = Anulada,
                DJMotos = DJMotos,
                FPago = FPago,
                CajaPago = CajaPago,
                CreditoFuturo = CreditoFuturo,
                NroCuotaOrden = NroCuotaOrden,
                NroConvenio = NroConvenio,
                NroCuotaOrdenAjuste = NroCuotaOrdenAjuste,
                ImpBoletaAnt = ImpBoletaAnt,
                ImpRecargoAnt = ImpRecargoAnt,
                TotalCreditoTomado = TotalCreditoTomado
            };
        }


        [Key]
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
        public int? IdUsuario { get; set; }
        public string Obs { get; set; }
        public bool Finalizado { get; set; }
        public decimal? ImpCuota { get; set; }
        public decimal? ImpRecargo { get; set; }
        public decimal? ImpDeduccion { get; set; }
        public decimal? MontoImponible { get; set; }
        public int? NroLiquidacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool ProcesadoMagic { get; set; }
        public bool Corregido { get; set; }
        public bool PM { get; set; }
        public DateTime? FechaPresentacion { get; set; }
        public int? UsrPresentacion { get; set; }
        public int? UsrAlta { get; set; }
        public DateTime FAlta { get; set; }
        public int? UsrModi { get; set; }
        public DateTime? FModi { get; set; }
        public int? UsrBaja { get; set; }
        public DateTime? FBaja { get; set; }
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

    public class DDJJCabeceraMetadata 
    {
        [Required]
        public int PeriodoMes { get; set; }

        [Required]
        public int PeriodoAnio { get; set; }

        [Required]
        public int NRO_INCRIPCION { get; set; }

        [Required]
        public DateTime FAlta { get; set; }

        public int? IdUsuario { get; set; }
        public string Obs { get; set; }
        public bool Finalizado { get; set; }
        public decimal? ImpCuota { get; set; }
        public decimal? ImpRecargo { get; set; }
        public decimal? ImpDeduccion { get; set; }
        public decimal? MontoImponible { get; set; }
        public int? NroLiquidacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool ProcesadoMagic { get; set; }
        public bool Corregido { get; set; }
        public bool PM { get; set; }
        public DateTime? FechaPresentacion { get; set; }
        public int? UsrPresentacion { get; set; }
        public int? UsrAlta { get; set; }
        public int? UsrModi { get; set; }
        public DateTime? FModi { get; set; }
        public int? UsrBaja { get; set; }
        public DateTime? FBaja { get; set; }
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
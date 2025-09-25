using System;
using System.Linq;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Services.Utils
{
    public class RutinasService
    {
        private readonly ApplicationDbContext _context;

        public RutinasService(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal ObtenerValorUCM(DateTime fecha) 
        {
            var config = _context.SIS_Configuracion
                .Where(c => c.Id == 1 && fecha >= c.FVigenciaDesde && fecha <= c.FVigenciaHasta)
                .OrderBy(c => c.FVigenciaDesde)
                .FirstOrDefault();

            return config?.ValorDec ?? 0;
        }

        public decimal CalcularImporteCuota(int cantUCMs, decimal valorUCM)
        {
            if (cantUCMs > 0)
            {
                decimal d = cantUCMs;
                return Redondeo(d * valorUCM);
            }
            return 0;
        }

        public decimal CalcularDescPagoContado(int cantUCMs, decimal valorUCM, int cuotas, decimal porc)
        {
            decimal importe = CalcularImporteCuota(cantUCMs, valorUCM);
            decimal total = importe * cuotas;
            return Redondeo(total * (porc / 100));
        }

        public decimal CalcularImportePagoContado(int cantUCMs, decimal valorUCM, int cuotas, decimal porc)
        {
            decimal importe = CalcularImporteCuota(cantUCMs, valorUCM);
            decimal total = importe * cuotas;
            return Redondeo(total - CalcularDescPagoContado(cantUCMs, valorUCM, cuotas, porc));
        }

        public decimal Redondeo(decimal d)
        {
            d = Math.Round(d, 3);
            d += 0.05m;
            return Math.Round(d, 1);
        }
        public DateTime ObtenerFVto(ContribuyenteDetalleMagic cuenta, CaratulaMagic caratula)
        {
            if (cuenta == null || caratula == null)
                return DateTime.MinValue;

            string nro = "";
            if (caratula.PERIODO_CAR.Year >= 2015)
                nro = cuenta.NRO_CUIT_DRI?.Trim() ?? "";
            else
                nro = cuenta.NRO_INCRIP_API.ToString().Trim();

            DateTime fvto;

            if (cuenta.CONV_MULTILATERAL == "S" && !string.IsNullOrEmpty(nro))
            {
                char ultimoDigito = nro[nro.Length - 1];
                switch (ultimoDigito)
                {
                    case '0': fvto = caratula.FVTO_API0_CAR; break;
                    case '1': fvto = caratula.FVTO_API1_CAR; break;
                    case '2': fvto = caratula.FVTO_API2_CAR; break;
                    case '3': fvto = caratula.FVTO_API3_CAR; break;
                    case '4': fvto = caratula.FVTO_API4_CAR; break;
                    case '5': fvto = caratula.FVTO_API5_CAR; break;
                    case '6': fvto = caratula.FVTO_API6_CAR; break;
                    case '7': fvto = caratula.FVTO_API7_CAR; break;
                    case '8': fvto = caratula.FVTO_API8_CAR; break;
                    default: fvto = caratula.FVTO_API9_CAR; break;
                }
            }
            else
            {
                fvto = caratula.F_VTO_CAR;
            }

            return fvto;
        }
    }
}
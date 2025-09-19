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
    }
}
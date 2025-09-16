using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Services
{
    public class RutinasService
    {
        public static decimal CalcularImporteCuota(int cantUCMs, decimal valorUCM)
        {
            // Implementa la lógica aquí
            return cantUCMs * valorUCM;
        }

        public static decimal CalcularImportePagoContado(int cantUCMs, decimal valorUCM, int cantidadCuotas, decimal porcDescPC)
        {
            var importeTotal = cantUCMs * valorUCM;
            var descuento = importeTotal * (porcDescPC / 100);
            return importeTotal - descuento;
        }
    }
}
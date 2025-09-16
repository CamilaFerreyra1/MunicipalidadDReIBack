using System;
using System.Collections.Generic;
using System.Linq;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Services
{
    public class CaratulaService
    {
        // Obtiene carátulas del año en curso
        public List<CaratulaMagic> ObtenerCaratulas()
        {
            var fecha = new DateTime(DateTime.Today.Year, 1, 1);
            return ObtenerCaratulas(fecha);
        }

        // Obtiene carátulas de un año determinado
        public List<CaratulaMagic> ObtenerCaratulas(int año)
        {
            var fDesde = new DateTime(año, 1, 1);
            var fHasta = new DateTime(año, 12, 31);
            return ObtenerCaratulas(fDesde, fHasta);
        }

        // Obtiene carátulas desde una fecha hasta fin de año
        public List<CaratulaMagic> ObtenerCaratulas(DateTime fecha)
        {
            var fHasta = new DateTime(fecha.Year, 12, 31);
            return ObtenerCaratulas(fecha, fHasta);
        }

        // Obtiene carátulas entre dos fechas
        public List<CaratulaMagic> ObtenerCaratulas(DateTime fDesde, DateTime fHasta)
        {
            using (var context = new DbContext())
            {
                return context.MAGIC_C03CARAA
                    .Where(c => c.F_BAJA_CAR.Year == 1900 && c.PERIODO_CAR >= fDesde && c.PERIODO_CAR <= fHasta)
                    .OrderBy(c => c.PERIODO_CAR)
                    .ToList();
            }
        }
    }
}
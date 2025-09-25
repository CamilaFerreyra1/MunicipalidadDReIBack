using System;
using System.Collections.Generic;
using System.Linq;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Services
{
    public class TablasService
    {
        private readonly ApplicationDbContext _context;
        private readonly NomencladorService _nomencladorService;

        public TablasService(ApplicationDbContext context, NomencladorService nomencladorService)
        {
            _context = context;
            _nomencladorService = nomencladorService;
        }

        public IQueryable<Rubros> Rubros(DateTime fDesde, DateTime fHasta)
        {
            return _context.DRI_Rubros
                .Where(c => (c.FAlta >= fDesde && c.FAlta <= fHasta) ||
                           (c.FModi >= fDesde && c.FModi <= fHasta) ||
                           (c.FBaja >= fDesde && c.FBaja <= fHasta));
        }

        public IQueryable<ExplotacionMagic> Actividades(DateTime fDesde, DateTime fHasta)
        {
            int nroNomenclador = ObtenerUltimoNomenclador();
            return _context.M03EXPLO1_orig
                .Where(a => a.NroNomenclador == nroNomenclador &&
                           ((a.FECHA_BAJA >= fDesde && a.FECHA_BAJA <= fHasta) ||
                            (a.FECHA_ALTA >= fDesde && a.FECHA_ALTA <= fHasta) ||
                            (a.FECHA_MODIF >= fDesde && a.FECHA_MODIF <= fHasta)));
        }

        public IQueryable<ContribuyenteDetalleMagic> Contribuyentes(DateTime fDesde, DateTime fHasta)
        {
            return _context.MAGIC_M03DRI
                .Where(a => (a.FECHABAJA >= fDesde && a.FECHABAJA <= fHasta) ||
                           (a.FECHAALTA >= fDesde && a.FECHAALTA <= fHasta) ||
                           (a.FECHAMODIF >= fDesde && a.FECHAMODIF <= fHasta));
        }

        public IQueryable<RubroActividad> RubrosActividades(DateTime fDesde, DateTime fHasta)
        {
            int nroNomenclador = ObtenerUltimoNomenclador();
            return _context.DRI_RubrosActividades
                .Where(c => c.NroNomenclador == nroNomenclador &&
                           ((c.FAlta >= fDesde && c.FAlta <= fHasta) ||
                            (c.FModi >= fDesde && c.FModi <= fHasta) ||
                            (c.FBaja >= fDesde && c.FBaja <= fHasta)));
        }

        public IQueryable<RubroContribuyente> RubrosContrib(DateTime fDesde, DateTime fHasta)
        {
            int nroNomenclador = ObtenerUltimoNomenclador();
            return _context.DRI_RubrosContrib
                .Where(c => c.NroNomenclador == nroNomenclador &&
                           ((c.FAlta >= fDesde && c.FAlta <= fHasta) ||
                            (c.FModi >= fDesde && c.FModi <= fHasta) ||
                            (c.FBaja >= fDesde && c.FBaja <= fHasta)));
        }

        private int ObtenerUltimoNomenclador()
        {
            // Implementa la lógica de NomencladoresBL.ObtenerUltimo()
            // Ejemplo: devuelve el NroNomenclador más alto
            return _context.DRI_Nomencladores.Max(n => n.NroNomenclador);
        }
    }
}
using System;
using System.Linq;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Services
{
    public class NomencladorService
    {
        private readonly ApplicationDbContext _context;

        public NomencladorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public int ObtenerUltimo()
        {
            return _context.DRI_Nomencladores
                .Where(n => n.VigenciaHasta > DateTime.Today)
                .OrderBy(n => n.VigenciaHasta)
                .Select(n => n.NroNomenclador)
                .FirstOrDefault();
        }
    }
}
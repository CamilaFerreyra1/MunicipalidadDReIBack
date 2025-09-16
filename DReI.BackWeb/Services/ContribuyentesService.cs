using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Data;

namespace DReI.BackWeb.Services
{
    public class ContribuyentesService
    {
        private readonly DbContext _context;

        public ContribuyentesService(DbContext context)
        {
            _context = context;
        }

        public ContribuyenteDetalleMagic Obtener(int cuenta, bool traerDadosDeBaja = false)
        {
            return _context.MAGIC_M03DRI
                .Where(c => c.NRO_INCRIPCION == cuenta &&
                           (traerDadosDeBaja || c.FECHABAJA.Year == 1900))
                .FirstOrDefault();
        }
    }
}

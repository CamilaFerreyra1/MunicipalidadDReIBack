using System;
using System.Linq;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;

namespace DReI.BackWeb.Services
{
    public class ConfiguracionTributariaService
    {
        private readonly ApplicationDbContext _context;

        public ConfiguracionTributariaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ConfiguracionTributariaDto> ObtenerLista()
        {
            return _context.DRI_Configuracion
                .Where(c => c.FBaja.Year == 1900)
                .OrderByDescending(c => c.FDesde.Year)
                .Select(c => new ConfiguracionTributariaDto
                {
                    IdConfig = c.IdConfig,
                    IdTipoCalc = c.IdTipoCalc,
                    FDesde = c.FDesde,
                    FHasta = c.FHasta,
                    Alicuota = c.Alicuota,
                    CantUCM = c.CantUCM
                });
        }

        public ConfiguracionTributariaDto ObtenerPorId(int idConfig)
        {
            return _context.DRI_Configuracion
                .Where(c => c.IdConfig == idConfig && c.FBaja.Year == 1900)
                .Select(c => new ConfiguracionTributariaDto
                {
                    IdConfig = c.IdConfig,
                    IdTipoCalc = c.IdTipoCalc,
                    FDesde = c.FDesde,
                    FHasta = c.FHasta,
                    Alicuota = c.Alicuota,
                    CantUCM = c.CantUCM
                })
                .FirstOrDefault();
        }
    }
}
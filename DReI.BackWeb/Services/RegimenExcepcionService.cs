using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;
using System.Data.Entity; 

namespace DReI.BackWeb.Services
{
    public class RegimenExcepcionService
    {
        private readonly ApplicationDbContext _context;

        public RegimenExcepcionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegimenExcepcionDto> ObtenerExcepcionAsync(int cuenta)
        {
            var fechaActual = DateTime.Today;
            var entidad = await _context.DRI_RegimenExepciones
                .Where(e => e.NRO_INCRIPCION == cuenta &&
                           e.FHasta >= fechaActual &&
                           e.FBaja.Year == 1900)
                .OrderBy(e => e.FHasta)
                .FirstOrDefaultAsync();

            return entidad != null ? MapToDTO(entidad) : null;
        }

        public async Task<List<RegimenExcepcionDto>> ObtenerListaAsync()
        {
            var entidades = await _context.DRI_RegimenExepciones
                .Where(e => e.FBaja.Year == 1900)
                .OrderBy(e => e.FAlta)
                .ToListAsync();

            return entidades.Select(MapToDTO).ToList();
        }

        public async Task CrearAsync(RegimenExcepcionDto dto, int idUsuario)
        {
            var entidad = new RegimenExcepcion
            {
                NRO_INCRIPCION = dto.NRO_INCRIPCION,
                FHasta = dto.FHasta,
                PermitirAdhesion = dto.PermitirAdhesion,
                ValorUCM = dto.ValorUCM,
                UsrAlta = idUsuario,
                FAlta = DateTime.Now,
                FBaja = new DateTime(1900, 1, 1),
                UsrBaja = 0
            };

            _context.DRI_RegimenExepciones.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int idExcepcion, int idUsuario)
        {
            var entidad = await _context.DRI_RegimenExepciones
                .FirstOrDefaultAsync(e => e.IdExcepcion == idExcepcion);

            if (entidad == null)
                throw new Exception("Excepción no encontrada.");

            entidad.UsrBaja = idUsuario;
            entidad.FBaja = DateTime.Now;

            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<RegimenExcepcionDto> ObtenerPorIdAsync(int idExcepcion)
        {
            var entidad = await _context.DRI_RegimenExepciones
                .Where(e => e.IdExcepcion == idExcepcion)
                .FirstOrDefaultAsync();

            return entidad != null ? MapToDTO(entidad) : null;
        }

        private RegimenExcepcionDto MapToDTO(RegimenExcepcion e)
        {
            if (e == null) return null;

            return new RegimenExcepcionDto
            {
                IdExcepcion = e.IdExcepcion,
                NRO_INCRIPCION = e.NRO_INCRIPCION,
                FHasta = e.FHasta,
                PermitirAdhesion = e.PermitirAdhesion,
                ValorUCM = e.ValorUCM,
                FAlta = e.FAlta,
                UsrAlta = e.UsrAlta,
                FBaja = e.FBaja.Year == 1900 ? null : (DateTime?)e.FBaja,
                UsrBaja = e.UsrBaja == 0 ? null : (int?)e.UsrBaja 
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Entities;
using System.Data.Entity;
using DReI.BackWeb.Services.Utils;

namespace DReI.BackWeb.Services
{
    public class MonotributoService
    {
        private readonly ApplicationDbContext _context;
        private readonly RutinasService _rutinasService;

        public MonotributoService(ApplicationDbContext context, RutinasService rutinasService)
        {
            _context = context;
            _rutinasService = rutinasService;
        }

        public async Task<List<MonotributoDto>> ListaAsync()
        {
            var lista = await _context.DRI_CatMonoContrib
                .Where(c => c.FBaja.Year == 1900)
                .ToListAsync();

            return lista.Select(MapToDTO).ToList();
        }

        public async Task<MonotributoDto> ObtenerAsync(int cuenta, DateTime periodo, bool traerDadosDeBaja = false)
        {
            var query = _context.DRI_CatMonoContrib
                .Where(c => c.NRO_INCRIPCION == cuenta &&
                            c.FVigenciaDesde <= periodo &&
                            c.FVigenciaHasta >= periodo);

            if (!traerDadosDeBaja)
                query = query.Where(c => c.FBaja.Year == 1900);

            var entidad = await query.FirstOrDefaultAsync();
            return entidad != null ? MapToDTO(entidad) : null;
        }

        public async Task CrearAsync(MonotributoDto dto, int idUsuario)
        {
            var entidad = new Contribuyente
            {
                NRO_INCRIPCION = dto.NRO_INCRIPCION,
                FVigenciaDesde = dto.FVigenciaDesde,
                //FVigenciaHasta = dto.FVigenciaHasta,
                IdVigencia = dto.IdVigencia,
                CantUCM = dto.CantUCM,
                ValorUCM = ObtenerValorUCM(dto.FVigenciaDesde),
                Email = dto.Email,
                TE = dto.TE,
                //FVerificado = dto.FVerificado,
                CUIT = dto.CUIT,
                //TIPO_RETENCION_DRI = dto.TIPO_RETENCION_DRI,

                UsrAlta = idUsuario,
                FAlta = DateTime.Now,
                FBaja = new DateTime(1900, 1, 1),
                UsrBaja = 0
            };

            _context.DRI_CatMonoContrib.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ModificarAsync(MonotributoDto dto, int idUsuario)
        {
            var entidad = await _context.DRI_CatMonoContrib
                .FirstOrDefaultAsync(c => c.NRO_INCRIPCION == dto.NRO_INCRIPCION &&
                                          c.FVigenciaDesde == dto.FVigenciaDesde);

            if (entidad == null)
                throw new Exception("Registro no encontrado.");

            entidad.IdVigencia = dto.IdVigencia;
            entidad.CantUCM = dto.CantUCM;
            entidad.ValorUCM = dto.ValorUCM;
            //entidad.TIPO_RETENCION_DRI = dto.TIPO_RETENCION_DRI;
            entidad.Email = dto.Email;
            entidad.TE = dto.TE;
            //entidad.FVerificado = dto.FVerificado;
            entidad.CUIT = dto.CUIT;

            entidad.UsrModi = idUsuario;
            entidad.FModi = DateTime.Now;

            _context.Entry(entidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int nroInscripcion, DateTime fVigenciaDesde, int idUsuario)
        {
            var entidad = await _context.DRI_CatMonoContrib
                .FirstOrDefaultAsync(c => c.NRO_INCRIPCION == nroInscripcion &&
                                          c.FVigenciaDesde == fVigenciaDesde);

            if (entidad == null)
                throw new Exception("Registro no encontrado.");

            var baja = new ContribuyenteBaja
            {
                NRO_INCRIPCION = entidad.NRO_INCRIPCION,
                FVigenciaDesde = entidad.FVigenciaDesde,
                FVigenciaHasta = entidad.FVigenciaHasta,
                IdVigencia = entidad.IdVigencia,
                CantUCM = entidad.CantUCM,
                ValorUCM = entidad.ValorUCM,
                Email = entidad.Email,
                TE = entidad.TE,
                FVerificado = entidad.FVerificado,
                CUIT = entidad.CUIT,
                //TIPO_RETENCION_DRI = entidad.TIPO_RETENCION_DRI,
                UsrAlta = entidad.UsrAlta,
                FAlta = entidad.FAlta,
                UsrModi = entidad.UsrModi,
                FModi = entidad.FModi,
                UsrBaja = entidad.UsrBaja == 0 ? idUsuario : entidad.UsrBaja,
                FBaja = entidad.FBaja.Year == 1900 ? DateTime.Now : entidad.FBaja
            };

            _context.DRI_CatMonoContrib_BAJAS.Add(baja);
            _context.DRI_CatMonoContrib.Remove(entidad);
            await _context.SaveChangesAsync();
        }

        private decimal ObtenerValorUCM(DateTime fecha)
        {
            return _rutinasService.ObtenerValorUCM(fecha);
        }

        private MonotributoDto MapToDTO(Contribuyente e)
        {
            if (e == null) return null;

            return new MonotributoDto
            {
                NRO_INCRIPCION = e.NRO_INCRIPCION,
                FVigenciaDesde = e.FVigenciaDesde,
                FVigenciaHasta = e.FVigenciaHasta,
                IdVigencia = e.IdVigencia,
                CantUCM = e.CantUCM,
                ValorUCM = e.ValorUCM,
                Email = e.Email,
                TE = e.TE,
                FVerificado = e.FVerificado,
                CUIT = e.CUIT,
                //TIPO_RETENCION_DRI = e.TIPO_RETENCION_DRI
            };
        }
    }
}
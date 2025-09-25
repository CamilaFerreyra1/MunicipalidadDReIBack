using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Responses; // 👈 Tu namespace real
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Services;

namespace DReI.BackWeb.Services
{
    public class RubroActividadService
    {
        private readonly ApplicationDbContext _context;
        private readonly NomencladorService _nomencladorService;
        private readonly RubroContribuyenteService _rubroContribuyenteService;

        public RubroActividadService(
            ApplicationDbContext context,
            NomencladorService nomencladorService,
            RubroContribuyenteService rubroContribuyenteService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _nomencladorService = nomencladorService ?? throw new ArgumentNullException(nameof(nomencladorService));
            _rubroContribuyenteService = rubroContribuyenteService ?? throw new ArgumentNullException(nameof(rubroContribuyenteService));
        }

        public async Task<Respuesta> ObtenerActividadesPorRubroAsync(int idRubro)
        {
            var respuesta = new Respuesta();

            try
            {
                int nroNomenclador = _nomencladorService.ObtenerUltimo();

                var codigosActividades = await (from ra in _context.DRI_RubrosActividades
                                                where ra.IdRubro == idRubro
                                                      && ra.FBaja.Year == 1900
                                                      && ra.NroNomenclador == nroNomenclador
                                                select ra.CodActividad).ToListAsync();

                var actividades = await (from a in _context.M03EXPLO1_orig
                                         where codigosActividades.Contains(a.COD_EXPLOTACION)
                                               && a.NroNomenclador == nroNomenclador
                                         orderby a.COD_EXPLOTACION
                                         select new RubroActividadDto
                                         {
                                             IdRubro = idRubro,
                                             CodActividad = a.COD_EXPLOTACION,
                                             Comuna = a.COMUNA,
                                             NroNomenclador = a.NroNomenclador
                                         }).ToListAsync();

                respuesta.Resultado = actividades;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError(ex.Message);
                return respuesta;
            }
        }

        public async Task<Respuesta> CrearAsync(RubroActividadCreateDto dto)
        {
            var respuesta = new Respuesta();

            if (dto == null || dto.CodigosActividades == null || !dto.CodigosActividades.Any())
            {
                respuesta.AgregarMensajeDeError("Datos inválidos: IdRubro, CodigosActividades o IdUsuario faltantes.");
                return respuesta;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int nroNomenclador = _nomencladorService.ObtenerUltimo();

                    var actividadesValidas = await (from a in _context.M03EXPLO1_orig
                                                    where a.COMUNA == 1
                                                          && dto.CodigosActividades.Contains(a.COD_EXPLOTACION)
                                                          && a.FECHA_BAJA.Year == 1900
                                                          && a.NroNomenclador == nroNomenclador
                                                    select a).ToListAsync();

                    if (!actividadesValidas.Any())
                    {
                        respuesta.AgregarMensajeDeError("No se encontraron actividades válidas para asignar.");
                        return respuesta;
                    }

                    foreach (var act in actividadesValidas)
                    {
                        var rubAct = new RubroActividad
                        {
                            COMUNA = act.COMUNA,
                            CodActividad = act.COD_EXPLOTACION,
                            NroNomenclador = act.NroNomenclador,
                            IdRubro = dto.IdRubro,
                            UsrAlta = dto.IdUsuario,
                            FAlta = DateTime.Now,
                            FBaja = new DateTime(1900, 1, 1)
                        };
                        _context.DRI_RubrosActividades.Add(rubAct);
                    }

                    await _context.SaveChangesAsync();

                    // Llamar al futuro servicio de RubroContribuyente
                    var respuestaContrib = await _rubroContribuyenteService.NuevoAsync(dto.IdRubro, actividadesValidas, dto.IdUsuario);
                    if (respuestaContrib.CantidadDeErrores > 0)
                    {
                        transaction.Rollback();
                        foreach (var error in respuestaContrib.Error)
                        {
                            respuesta.AgregarMensajeDeError(error?.ToString());
                        }
                        return respuesta;
                    }

                    transaction.Commit();
                    respuesta.Resultado = "OK";
                    return respuesta;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    respuesta.AgregarMensajeDeError(ex.InnerException?.InnerException?.Message ?? ex.Message);
                    return respuesta;
                }
            }
        }

        public async Task<Respuesta> EliminarAsync(RubroActividadCreateDto dto)
        {
            var respuesta = new Respuesta();

            if (dto == null || dto.CodigosActividades == null || !dto.CodigosActividades.Any())
            {
                respuesta.AgregarMensajeDeError("Datos inválidos: IdRubro, CodigosActividades o IdUsuario faltantes.");
                return respuesta;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var respuestaContrib = await _rubroContribuyenteService.EliminarAsync(dto.IdRubro, dto.CodigosActividades, dto.IdUsuario);
                    if (respuestaContrib.CantidadDeErrores > 0)
                    {
                        foreach (var error in respuestaContrib.Error)
                        {
                            respuesta.AgregarMensajeDeError(error?.ToString());
                        }
                        return respuesta;
                    }

                    var registrosActivos = await (from ra in _context.DRI_RubrosActividades
                                                  where ra.IdRubro == dto.IdRubro
                                                        && dto.CodigosActividades.Contains(ra.CodActividad)
                                                        && ra.FBaja.Year == 1900
                                                  select ra).ToListAsync();

                    if (!registrosActivos.Any())
                    {
                        respuesta.AgregarMensajeDeError("No se encontraron actividades activas para eliminar.");
                        return respuesta;
                    }

                    foreach (var registro in registrosActivos)
                    {
                        registro.FBaja = DateTime.Now;
                        registro.UsrBaja = dto.IdUsuario;
                        _context.Entry(registro).State = EntityState.Modified;
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                    respuesta.Resultado = "OK";
                    return respuesta;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    respuesta.AgregarMensajeDeError(ex.InnerException?.InnerException?.Message ?? ex.Message);
                    return respuesta;
                }
            }
        }
    }
}
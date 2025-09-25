using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Responses;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Services;

namespace DReI.BackWeb.Services
{
    public class RubroService
    {
        private readonly ApplicationDbContext _context;
        private readonly RubroActividadService _rubroActividadService;

        public RubroService(
            ApplicationDbContext context,
            RubroActividadService rubroActividadService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _rubroActividadService = rubroActividadService ?? throw new ArgumentNullException(nameof(rubroActividadService));
        }

        public async Task<Respuesta> ObtenerTodosAsync()
        {
            var respuesta = new Respuesta();
            try
            {
                var rubros = await _context.DRI_Rubros
                    .Where(r => r.FBaja.Year == 1900)
                    .OrderBy(r => r.Descripcion)
                    .Select(r => new RubroDto
                    {
                        IdRubro = r.IdRubro,
                        Descripcion = r.Descripcion
                    })
                    .ToListAsync();

                respuesta.Resultado = rubros;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError(ex.Message);
                return respuesta;
            }
        }

        public async Task<Respuesta> ObtenerPorIdAsync(int id)
        {
            var respuesta = new Respuesta();
            try
            {
                var rubro = await _context.DRI_Rubros
                    .Where(r => r.IdRubro == id)
                    .Select(r => new RubroDto
                    {
                        IdRubro = r.IdRubro,
                        Descripcion = r.Descripcion
                    })
                    .FirstOrDefaultAsync();

                if (rubro == null)
                {
                    respuesta.AgregarMensajeDeError("Rubro no encontrado.");
                }
                else
                {
                    respuesta.Resultado = rubro;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError(ex.Message);
                return respuesta;
            }
        }

        public async Task<Respuesta> CrearAsync(RubroCreateDto dto)
        {
            var respuesta = new Respuesta();

            if (string.IsNullOrWhiteSpace(dto.Descripcion) || dto.CodigosActividades == null)
            {
                respuesta.AgregarMensajeDeError("Datos inválidos: Descripción o actividades faltantes.");
                return respuesta;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var rubro = new Rubros
                    {
                        Descripcion = dto.Descripcion,
                        UsrAlta = dto.IdUsuario,
                        FAlta = DateTime.Now,
                        FBaja = new DateTime(1900, 1, 1)
                    };
                    // rubro.ValoresPorDefecto(); // si existe, llámalo
                    _context.DRI_Rubros.Add(rubro);
                    await _context.SaveChangesAsync();

                    // Crear relaciones con actividades
                    var respuestaActividades = await _rubroActividadService.CrearAsync(new RubroActividadCreateDto
                    {
                        IdRubro = rubro.IdRubro,
                        CodigosActividades = dto.CodigosActividades,
                        IdUsuario = dto.IdUsuario
                    });

                    if (respuestaActividades.CantidadDeErrores > 0)
                    {
                        transaction.Rollback();
                        foreach (var error in respuestaActividades.Error)
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

        public async Task<Respuesta> ModificarAsync(RubroUpdateDto dto)
        {
            var respuesta = new Respuesta();

            if (dto.IdRubro <= 0 || string.IsNullOrWhiteSpace(dto.Descripcion) || dto.CodigosActividades == null)
            {
                respuesta.AgregarMensajeDeError("Datos inválidos.");
                return respuesta;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var rubro = await _context.DRI_Rubros
                        .FirstOrDefaultAsync(r => r.IdRubro == dto.IdRubro && r.FBaja.Year == 1900);

                    if (rubro == null)
                    {
                        respuesta.AgregarMensajeDeError("Rubro no encontrado o dado de baja.");
                        return respuesta;
                    }

                    bool actualizar = false;

                    // Actualizar descripción si cambió
                    if (rubro.Descripcion != dto.Descripcion)
                    {
                        rubro.Descripcion = dto.Descripcion;
                        rubro.UsrModi = dto.IdUsuario;
                        rubro.FModi = DateTime.Now;
                        _context.Entry(rubro).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        actualizar = true;
                    }

                    // Obtener actividades actuales
                    var actividadesActuales = await _context.DRI_RubrosActividades
                        .Where(ra => ra.IdRubro == dto.IdRubro && ra.FBaja.Year == 1900)
                        .Select(ra => ra.CodActividad)
                        .ToListAsync();

                    var actividadesNuevas = dto.CodigosActividades.Except(actividadesActuales).ToList();
                    var actividadesAQuitar = actividadesActuales.Except(dto.CodigosActividades).ToList();

                    string error = "";

                    if (actividadesAQuitar.Any())
                    {
                        var respEliminar = await _rubroActividadService.EliminarAsync(new RubroActividadCreateDto
                        {
                            IdRubro = dto.IdRubro,
                            CodigosActividades = actividadesAQuitar,
                            IdUsuario = dto.IdUsuario
                        });

                        if (respEliminar.CantidadDeErrores > 0)
                        {
                            error = string.Join("; ", respEliminar.Error.Select(e => e?.ToString()));
                        }
                        else
                        {
                            actualizar = true;
                        }
                    }

                    if (string.IsNullOrEmpty(error) && actividadesNuevas.Any())
                    {
                        var respCrear = await _rubroActividadService.CrearAsync(new RubroActividadCreateDto
                        {
                            IdRubro = dto.IdRubro,
                            CodigosActividades = actividadesNuevas,
                            IdUsuario = dto.IdUsuario
                        });

                        if (respCrear.CantidadDeErrores > 0)
                        {
                            error = string.Join("; ", respCrear.Error.Select(e => e?.ToString()));
                        }
                        else
                        {
                            actualizar = true;
                        }
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        transaction.Rollback();
                        respuesta.AgregarMensajeDeError(error);
                        return respuesta;
                    }

                    if (actualizar)
                    {
                        transaction.Commit();
                        respuesta.Resultado = "OK";
                    }
                    else
                    {
                        // Nada que actualizar
                        respuesta.Resultado = "OK";
                    }

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

        public async Task<Respuesta> EliminarAsync(int idRubro, int idUsuario)
        {
            var respuesta = new Respuesta();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var rubro = await _context.DRI_Rubros
                        .FirstOrDefaultAsync(r => r.IdRubro == idRubro && r.FBaja.Year == 1900);

                    if (rubro == null)
                    {
                        respuesta.AgregarMensajeDeError("Rubro no encontrado.");
                        return respuesta;
                    }

                    // Obtener todas las actividades del rubro
                    var actividadesAQuitar = await _context.DRI_RubrosActividades
                        .Where(ra => ra.IdRubro == idRubro && ra.FBaja.Year == 1900)
                        .Select(ra => ra.CodActividad)
                        .ToListAsync();

                    if (actividadesAQuitar.Any())
                    {
                        var respEliminar = await _rubroActividadService.EliminarAsync(new RubroActividadCreateDto
                        {
                            IdRubro = idRubro,
                            CodigosActividades = actividadesAQuitar,
                            IdUsuario = idUsuario
                        });

                        if (respEliminar.CantidadDeErrores > 0)
                        {
                            transaction.Rollback();
                            foreach (var error in respEliminar.Error)
                            {
                                respuesta.AgregarMensajeDeError(error?.ToString());
                            }
                            return respuesta;
                        }
                    }

                    // Dar de baja el rubro
                    rubro.FBaja = DateTime.Now;
                    rubro.UsrBaja = idUsuario;
                    _context.Entry(rubro).State = EntityState.Modified;
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
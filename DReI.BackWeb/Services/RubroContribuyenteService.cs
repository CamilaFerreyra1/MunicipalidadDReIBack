using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Responses;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;

namespace DReI.BackWeb.Services
{
    public class RubroContribuyenteService
    {
        private readonly ApplicationDbContext _context;
        private readonly NomencladorService _nomencladorService;

        public RubroContribuyenteService(
            ApplicationDbContext context,
            NomencladorService nomencladorService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _nomencladorService = nomencladorService ?? throw new ArgumentNullException(nameof(nomencladorService));
        }

        /// <summary>
        /// Crea relaciones entre rubro y contribuyentes basadas en actividades.
        /// </summary>
        public async Task<Respuesta> NuevoAsync(int idRubro, List<ExplotacionMagic> actividades, int idUsuario)
        {
            var respuesta = new Respuesta();

            if (actividades == null || !actividades.Any())
            {
                respuesta.AgregarMensajeDeError("La lista de actividades no puede estar vacía.");
                return respuesta;
            }

            try
            {
                // Extraer valores únicos para filtrar en la BD
                var codigosActividades = actividades.Select(a => a.COD_EXPLOTACION).Distinct().ToList();
                var nroNomenclador = actividades.First().NroNomenclador; // asumimos que todos tienen el mismo

                // Obtener registros relacionados desde la BD
                var acList = await (from ac in _context.DRI_ActividadesContrib
                                    where codigosActividades.Contains(ac.CodActividad)
                                          && ac.NroNomenclador == nroNomenclador
                                    select ac).ToListAsync();

                var nrosInscripcion = acList.Select(ac => ac.NRO_INCRIPCION).Distinct().ToList();
                var comunasAc = acList.Select(ac => ac.COMUNA).Distinct().ToList();

                var cList = await (from c in _context.MAGIC_M03DRI
                                   where nrosInscripcion.Contains(c.NRO_INCRIPCION)
                                         && comunasAc.Contains(c.COMUNA)
                                         && c.FECHABAJA.Year == 1900
                                         && c.FEC_CLAUSURA.Year == 1900
                                   select c).ToListAsync();

                // Ahora hacer el join en memoria (LINQ to Objects)

                // Realizar el join entre actividades, DRI_ActividadesContrib y MAGIC_M03DRI
                var datos = (from a in actividades
                             join ac in _context.DRI_ActividadesContrib.ToList()
                                 on new { COMUNA = (int)a.COMUNA, a.COD_EXPLOTACION, a.NroNomenclador }
                                 equals new { ac.COMUNA, COD_EXPLOTACION = ac.CodActividad, ac.NroNomenclador }
                             join c in _context.MAGIC_M03DRI.ToList() 
                                 on new { COMUNA = (short)ac.COMUNA, ac.NRO_INCRIPCION }
                                 equals new { c.COMUNA, c.NRO_INCRIPCION }
                             where ac.FFin.Year == 1900
                                   && ac.FBaja == null
                                   && c.FECHABAJA.Year == 1900
                                   && c.FEC_CLAUSURA.Year == 1900
                             select new
                             {
                                 a.COMUNA,
                                 a.COD_EXPLOTACION,
                                 a.NroNomenclador,
                                 c.NRO_INCRIPCION
                             }).ToList(); 

                if (!datos.Any())
                {
                    respuesta.AgregarMensajeDeError("No se encontraron contribuyentes asociados a las actividades.");
                    return respuesta;
                }

                foreach (var d in datos)
                {
                    var rubroContrib = new RubroContribuyente
                    {
                        COMUNA = d.COMUNA,
                        CodActividad = d.COD_EXPLOTACION,
                        NroNomenclador = d.NroNomenclador,
                        NRO_INCRIPCION = d.NRO_INCRIPCION,
                        IdRubro = idRubro,
                        UsrAlta = idUsuario,
                        FAlta = DateTime.Now,
                        FBaja = new DateTime(1900, 1, 1) // activo
                    };
                    // rubroContrib.ValoresPorDefecto(); // si existe, llámalo
                    _context.DRI_RubrosContrib.Add(rubroContrib);
                }

                await _context.SaveChangesAsync();
                respuesta.Resultado = "OK";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError(ex.InnerException?.InnerException?.Message ?? ex.Message);
                return respuesta;
            }
        }

        /// <summary>
        /// Elimina relaciones de rubro-contribuyente por lista de actividades (soft delete + histórico).
        /// </summary>
        public async Task<Respuesta> EliminarAsync(int idRubro, List<int> codigosActividades, int idUsuario)
        {
            var respuesta = new Respuesta();

            if (codigosActividades == null || !codigosActividades.Any())
            {
                respuesta.AgregarMensajeDeError("La lista de códigos de actividades no puede estar vacía.");
                return respuesta;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int nroNomenclador = _nomencladorService.ObtenerUltimo();

                    var registrosActivos = await (from rc in _context.DRI_RubrosContrib
                                                  where rc.IdRubro == idRubro
                                                        && codigosActividades.Contains(rc.CodActividad)
                                                        && rc.FBaja.Year == 1900
                                                        && rc.NroNomenclador == nroNomenclador
                                                  select rc).ToListAsync();

                    if (!registrosActivos.Any())
                    {
                        respuesta.AgregarMensajeDeError("No se encontraron registros activos para eliminar.");
                        return respuesta;
                    }

                    foreach (var registro in registrosActivos)
                    {
                        // Marcar como dado de baja
                        registro.FBaja = DateTime.Now;
                        registro.UsrBaja = idUsuario;
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

        /// <summary>
        /// Elimina una relación específica rubro-contribuyente.
        /// </summary>
        public async Task<Respuesta> EliminarPorInscripcionAsync(int idRubro, int codActividad, int nroInscripcion, int idUsuario)
        {
            var respuesta = new Respuesta();

            try
            {
                int nroNomenclador = _nomencladorService.ObtenerUltimo();

                var registro = await (from rc in _context.DRI_RubrosContrib
                                      where rc.IdRubro == idRubro
                                            && rc.CodActividad == codActividad
                                            && rc.NRO_INCRIPCION == nroInscripcion
                                            && rc.FBaja.Year == 1900
                                            && rc.NroNomenclador == nroNomenclador
                                      select rc).FirstOrDefaultAsync();

                if (registro == null)
                {
                    respuesta.AgregarMensajeDeError("Registro no encontrado.");
                    return respuesta;
                }

                registro.FBaja = DateTime.Now;
                registro.UsrBaja = idUsuario;
                _context.Entry(registro).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                respuesta.Resultado = "OK";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.AgregarMensajeDeError(ex.InnerException?.InnerException?.Message ?? ex.Message);
                return respuesta;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Services.Utils;

namespace DReI.BackWeb.Services
{
    public class RegimenSimplificadoService
    {
        private readonly ApplicationDbContext _context;
        private readonly RestService _restService;
        private readonly RutinasService _rutinasService;
        private readonly CaratulaService _caratulaService;

        public RegimenSimplificadoService
            (ApplicationDbContext context,
            RestService restService,
            RutinasService rutinasService,
            CaratulaService caratulaService)

        {
            _context = context;
            _restService = restService;
            _rutinasService = rutinasService;
            _caratulaService = caratulaService;
        }

        public string AdherirAlRegimen(int cuenta, DateTime periodoDesde, string email, string te, string cuit, VigenciaDto vigencia, int usr)
        {
            string msg = "";

            var estado = ObtenerEstado(cuenta, periodoDesde);

            if (!estado.Adherido)
            {
                if (estado.FTopeAdhesion < DateTime.Today)
                {
                    msg = "No puede adherirse al regimen en este momento.";
                }
            }

            if (string.IsNullOrEmpty(msg))
            {
                var contribuyente = _context.MAGIC_M03DRI.FirstOrDefault(c => c.NRO_INCRIPCION == cuenta);
                var caratulas = ObtenerCaratulas(periodoDesde, contribuyente?.FEC_INICIO ?? periodoDesde);

                decimal valorUCM = ObtenerValorUCM(cuenta, DateTime.Today);
                decimal impCuota = CalcularImporteCuota(vigencia.CantUCMs, valorUCM);

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Eliminar adherencias anteriores
                        var adherenciaAnterior = _context.DRI_CatMonoContrib.FirstOrDefault(c => c.NRO_INCRIPCION == cuenta && c.FVigenciaDesde.Year == periodoDesde.Year);
                        if (adherenciaAnterior != null)
                        {
                            _context.DRI_CatMonoContrib.Remove(adherenciaAnterior);
                        }

                        // Crear nueva adherencia
                        var nuevaAdherencia = new Contribuyente
                        {
                            NRO_INCRIPCION = cuenta,
                            FVigenciaDesde = periodoDesde,
                            FVigenciaHasta = vigencia.FVigenciaHasta,
                            IdVigencia = vigencia.IdVigencia,
                            CantUCM = vigencia.CantUCMs,
                            ValorUCM = valorUCM,
                            Email = email,
                            TE = te,
                            CUIT = cuit,
                            FAlta = DateTime.Now,
                            UsrAlta = usr,
                            FModi = new DateTime(1900, 1, 1),
                            UsrModi = 0,
                            FBaja = new DateTime(1900, 1, 1),
                            UsrBaja = 0,
                            FVerificado = new DateTime(1900, 1, 1)
                        };

                        _context.DRI_CatMonoContrib.Add(nuevaAdherencia);

                        var declaraciones = new List<DDJJCabecera>();

                        foreach (var caratula in caratulas)
                        {
                            var existeDeclaracion = _context.DRI_DJ_CAB.Any(d => d.NRO_INCRIPCION == cuenta && d.PeriodoAnio == caratula.PERIODO_CAR.Year && d.PeriodoMes == caratula.PERIODO_CAR.Month && d.MontoImponible == 0);

                            if (!existeDeclaracion)
                            {
                                var nuevaDeclaracion = new DDJJCabecera
                                {
                                    PeriodoMes = caratula.PERIODO_CAR.Month,
                                    PeriodoAnio = caratula.PERIODO_CAR.Year,
                                    FVtoOriginal = (caratula.PERIODO_CAR.Month == 1 ? vigencia.FVto1erPeriodo : ObtenerFVto(contribuyente, caratula)),
                                    FechaVencimiento = caratula.F_VTO_CAR,
                                    TipoLiquidacion = "E",
                                    NRO_INCRIPCION = cuenta,
                                    Secuencia = 0,
                                    ImpCuota = impCuota,
                                    Finalizado = true,
                                    FechaPresentacion = DateTime.Now,
                                    UsrPresentacion = usr,
                                    FAlta = DateTime.Now,
                                    UsrAlta = usr,
                                    FModi = new DateTime(1900, 1, 1),
                                    UsrModi = 0,
                                    FBaja = new DateTime(1900, 1, 1),
                                    UsrBaja = 0
                                };

                                _context.DRI_DJ_CAB.Add(nuevaDeclaracion);
                                _context.SaveChanges(); // Necesario para obtener IdDJ

                                var nuevoRenglon = new DDJJRenglon
                                {
                                    IdDj = nuevaDeclaracion.IdDJ,
                                    IdTipoCal = 3,
                                    MontoFijoMensual = impCuota,
                                    Descripcion = $"Regimen Simplificado {periodoDesde.Year} - cuota {declaraciones.Count + 1} / {caratulas.Count}",
                                    FAlta = DateTime.Now,
                                    UsrAlta = usr,
                                    //FModi = new DateTime(1900, 1, 1),
                                    //UsrModi = 0,
                                    FBaja = new DateTime(1900, 1, 1),
                                    UsrBaja = 0
                                };

                                _context.DRI_DJ_REN.Add(nuevoRenglon);

                                // Aquí iría la llamada al WebService para generar la liquidación
                                // var resultado = _restService.POST<Respuesta>($"{ConfigurationManager.AppSettings["ServerWSMuni"]}Tributos/General/DRI/GenerarLiquidacion", "", new { /* datos */ });
                                // if (resultado.CantidadDeErrores == 0) { /* actualizar nuevaDeclaracion */ }
                            }
                        }

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        msg = ex.Message;
                    }
                }
            }

            return msg;
        }

        public RegimenSimplificadoDto ObtenerEstado(int cuenta, DateTime periodo)
        {
            var estado = new RegimenSimplificadoDto
            {
                NRO_INCRIPCION = cuenta,
                Verificado = false,
                TieneDeuda = false,
                FTopeAdhesion = new DateTime(1900, 1, 1),
                PeriodosEnRegimen = new List<DateTime>()
            };

            var vigencias = _context.DRI_CatMonoVigencias.ToList();
            if (vigencias.Count == 0) return null;

            var adherencia = _context.DRI_CatMonoContrib.FirstOrDefault(c => c.NRO_INCRIPCION == cuenta && c.FVigenciaDesde.Year == periodo.Year);
            estado.Adherido = (adherencia != null);

            if (estado.Adherido)
            {
                estado.CategoriaSeleccionada = new VigenciaDto
                {
                    IdVigencia = adherencia.IdVigencia,
                    Categoria = _context.DRI_CatMonoVigencias.First(v => v.IdVigencia == adherencia.IdVigencia).Categoria,
                    FVigenciaDesde = adherencia.FVigenciaDesde,
                    FVigenciaHasta = adherencia.FVigenciaHasta,
                    ValorUCM = adherencia.ValorUCM,
                    CantUCMs = adherencia.CantUCM
                };

                if (adherencia.FVigenciaDesde.Month == adherencia.FVigenciaHasta.Month)
                    estado.Mensaje = $"El contribuyente está adherido al Régimen Simplificado {periodo.Year} en el período {adherencia.FVigenciaDesde:MM/yyyy}.";
                else
                    estado.Mensaje = $"El contribuyente está adherido al Régimen Simplificado {periodo.Year} desde el período {adherencia.FVigenciaDesde:MM/yyyy} al período {adherencia.FVigenciaHasta:MM/yyyy}.";
            }
            else
            {
                // Lógica para verificar si puede adherir (similar al legacy)
                estado.Mensaje = "Puede adherirse al régimen simplificado.";
            }

            return estado;
        }

        private List<CaratulaMagic> ObtenerCaratulas(DateTime fDesde, DateTime fIngreso)
        {
            if (fIngreso > fDesde)
                fDesde = new DateTime(fIngreso.Year, fIngreso.Month, 1);

            return _caratulaService.ObtenerCaratulas(fDesde, new DateTime(fDesde.Year, 12, 31));
        }

        private decimal ObtenerValorUCM(int cuenta, DateTime fecha)
        {
            var excepcion = _context.DRI_RegimenExepciones.FirstOrDefault(e => e.NRO_INCRIPCION == cuenta && e.FHasta >= fecha);
            if (excepcion != null && excepcion.PermitirAdhesion)
                return excepcion.ValorUCM;

            return _rutinasService.ObtenerValorUCM(fecha);
        }

        private decimal CalcularImporteCuota(int cantUCMs, decimal valorUCM)
        {
            return _rutinasService.CalcularImporteCuota(cantUCMs, valorUCM);
        }

        private DateTime ObtenerFVto(ContribuyenteDetalleMagic contribuyente, CaratulaMagic caratula)
        {
            return _rutinasService.ObtenerFVto(contribuyente, caratula);
        }
    }
}
using System;
using System.Web.Http;
using DReI.BackWeb.Services;
using DReI.BackWeb.Models;
using DReI.BackWeb.Models.Entities;
using System.Linq;
using DReI.BackWeb.Data;

namespace DReI.BackWeb.Controllers
{
    [RoutePrefix("api/declaraciones")]
    public class DeclaracionesController : ApiController
    {
        [HttpGet]
        [Route("cuenta/{cuenta}/anio/{anio}")]
        [AllowAnonymous]
        public IHttpActionResult ObtenerPorCuentaYAnio(int cuenta, int anio)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var service = new DeclaracionesService(context);
                var declaraciones = service.ObtenerDeclaracionesPorCuentaYAnio(cuenta, anio)
                    .Select(d => new DeclaracionesDTO
                    {
                        IdDJ = d.IdDJ,
                        PeriodoMes = d.PeriodoMes,
                        PeriodoAnio = d.PeriodoAnio,
                        Secuencia = d.Secuencia,
                        RectEspecial = d.RectEspecial,
                        DerOcDomPub = d.DerOcDomPub,
                        TotalVentasPais = d.TotalVentasPais,
                        CoeficienteConvMult = d.CoeficienteConvMult,
                        OtrosPagos = d.OtrosPagos,
                        SaldoFavor = d.SaldoFavor,
                        PeriodoMesFavor = d.PeriodoMesFavor,
                        PeriodoAnioFavor = d.PeriodoAnioFavor,
                        NRO_INCRIPCION = d.NRO_INCRIPCION,
                        Obs = d.Obs,
                        Finalizado = d.Finalizado,
                        ImpCuota = d.ImpCuota,
                        ImpRecargo = d.ImpRecargo,
                        ImpDeduccion = d.ImpDeduccion,
                        MontoImponible = d.MontoImponible,
                        NroLiquidacion = d.NroLiquidacion,
                        FechaVencimiento = d.FechaVencimiento,
                        FechaPresentacion = d.FechaPresentacion,
                        ProcesadoMagic = d.ProcesadoMagic,
                        Corregido = d.Corregido,
                        PM = d.PM,
                        NroNomenclador = d.NroNomenclador,
                        IP = d.IP,
                        FVtoOriginal = d.FVtoOriginal,
                        IdDjOriginal = d.IdDjOriginal,
                        TipoLiquidacion = d.TipoLiquidacion,
                        MotivoBaja = d.MotivoBaja,
                        Anulada = d.Anulada,
                        DJMotos = d.DJMotos,
                        FPago = d.FPago,
                        CajaPago = d.CajaPago,
                        CreditoFuturo = d.CreditoFuturo,
                        NroCuotaOrden = d.NroCuotaOrden,
                        NroConvenio = d.NroConvenio,
                        NroCuotaOrdenAjuste = d.NroCuotaOrdenAjuste,
                        ImpBoletaAnt = d.ImpBoletaAnt,
                        ImpRecargoAnt = d.ImpRecargoAnt,
                        TotalCreditoTomado = d.TotalCreditoTomado
                    })
                    .ToList();

                return Ok(declaraciones);
            }
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public IHttpActionResult Crear(DeclaracionesDTO dto)
        {
            using (var context = DbContextFactory.CreateRafaelaContext())
            {
                var service = new DeclaracionesService(context);

                var nuevaDeclaracion = new DDJJCabecera
                {
                    NRO_INCRIPCION = dto.NRO_INCRIPCION,
                    PeriodoAnio = dto.PeriodoAnio,
                    TipoLiquidacion = dto.TipoLiquidacion,
                    FechaPresentacion = dto.FechaPresentacion
                    // ... mapea los campos necesarios
                };

                service.CrearDeclaracion(nuevaDeclaracion);

                // Opcional: devolver el DTO creado con ID
                return Ok(new
                {
                    Id = nuevaDeclaracion.IdDJ,
                    Mensaje = "Declaración creada correctamente"
                });
            }
        }
    }
}
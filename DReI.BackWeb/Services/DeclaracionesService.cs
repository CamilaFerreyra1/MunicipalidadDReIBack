using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Data;

namespace DReI.BackWeb.Services
{
    public class DeclaracionesService
    {
        private readonly ApplicationDbContext _context;

        public DeclaracionesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CrearDeclaracion(DDJJCabecera declaracion)
        {
            _context.DRI_DJ_CAB.Add(declaracion);
            _context.SaveChanges();
        }

        public void CrearRenglon(DDJJRenglon renglon)
        {
            _context.DRI_DJ_REN.Add(renglon);
            _context.SaveChanges();
        }

        public void ModificarDeclaracion(DDJJCabecera declaracion)
        {
            var existente = _context.DRI_DJ_CAB.Find(declaracion.IdDJ); 

            if (existente == null)
                throw new Exception("Declaración no encontrada");

            // Ver bien que campos debo mapear 
            existente.NRO_INCRIPCION = declaracion.NRO_INCRIPCION;
            existente.PeriodoAnio = declaracion.PeriodoAnio;
            existente.TipoLiquidacion = declaracion.TipoLiquidacion;
            

            _context.SaveChanges();
        }

        public IQueryable<DDJJCabecera> ObtenerDeclaraciones()
        {
            return from d in _context.DRI_DJ_CAB
                   where !d.FBaja.HasValue
                   select d;
        }

        public IQueryable<DDJJCabecera> ObtenerDeclaracionesPorCuentaYAnio(int cuenta, int anio)
        {
            return from d in ObtenerDeclaraciones()
                   where d.NRO_INCRIPCION == cuenta
                   && d.PeriodoAnio == anio
                   && d.TipoLiquidacion == "E"
                   select d;
        }
    }
}
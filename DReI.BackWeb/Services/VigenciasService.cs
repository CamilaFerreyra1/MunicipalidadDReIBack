using System;
using System.Linq;
using System.Collections.Generic;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;
using DReI.BackWeb.Models.Dto;
using DReI.BackWeb.Services.Utils;

namespace DReI.BackWeb.Services
{
    public class VigenciasService
    {
        private readonly ApplicationDbContext _context;
        private readonly RutinasService _rutinasService;

        public VigenciasService(ApplicationDbContext context, RutinasService rutinasService)
        {
            _context = context;
            _rutinasService = rutinasService;
        }

        public List<VigenciaDto> ObtenerVigencias(DateTime fecha, int cuenta)
        {
            decimal valorUCM = ObtenerValorUCM(cuenta, fecha);

            var vigencias = _context.DRI_CatMonoVigencias
                .Where(v => v.FVigenciaDesde <= fecha && fecha <= v.FVigenciaHasta && v.FBaja.Year == 1900)
                .OrderBy(v => v.Categoria)
                .ToList();

            foreach (var v in vigencias)
            {
                v.ValorUCM = valorUCM;
                // Aquí podrías agregar lógica de CantidadCuotas si la necesitas
            }

            return vigencias.Select(v => new VigenciaDto
            {
                IdVigencia = v.IdVigencia,
                Categoria = v.Categoria,
                CantUCMs = v.CantUCMs,
                PorcDescPC = v.PorcDescPC,
                FVigenciaDesde = v.FVigenciaDesde,
                FVigenciaHasta = v.FVigenciaHasta,
                ValorUCM = v.ValorUCM,
                CantidadCuotas = v.CantidadCuotas
            }).ToList();
        }

        public List<VigenciasRegimenDto> ObtenerRegimenes()
        {
            return _context.DRI_CatMonoVigencias
                .Where(v => v.FBaja.Year == 1900)
                .GroupBy(v => new { v.FVigenciaDesde, v.FVigenciaHasta })
                .Select(g => new VigenciasRegimenDto
                {
                    FDesde = g.Key.FVigenciaDesde,
                    FHasta = g.Key.FVigenciaHasta,
                    Categorias = g.Select(v => new VigenciaDto
                    {
                        IdVigencia = v.IdVigencia,
                        Categoria = v.Categoria,
                        CantUCMs = v.CantUCMs,
                        PorcDescPC = v.PorcDescPC,
                        FVigenciaDesde = v.FVigenciaDesde,
                        FVigenciaHasta = v.FVigenciaHasta,
                        ValorUCM = v.ValorUCM,
                        CantidadCuotas = v.CantidadCuotas
                    }).ToList()
                })
                .ToList();
        }

        public VigenciaDto ObtenerPorId(int idVigencia)
        {
            var vigencia = _context.DRI_CatMonoVigencias.Find(idVigencia);
            if (vigencia == null) return null;

            return new VigenciaDto
            {
                IdVigencia = vigencia.IdVigencia,
                Categoria = vigencia.Categoria,
                CantUCMs = vigencia.CantUCMs,
                PorcDescPC = vigencia.PorcDescPC,
                FVigenciaDesde = vigencia.FVigenciaDesde,
                FVigenciaHasta = vigencia.FVigenciaHasta,
                ValorUCM = vigencia.ValorUCM,
                CantidadCuotas = vigencia.CantidadCuotas
            };
        }

        public VigenciaDto CrearVigencia(VigenciaDto dto, int idUsuario)
        {
            var nuevaVigencia = new Vigencias
            {
                Categoria = dto.Categoria,
                CantUCMs = dto.CantUCMs,
                PorcDescPC = dto.PorcDescPC,
                FVigenciaDesde = dto.FVigenciaDesde,
                FVigenciaHasta = dto.FVigenciaHasta,
                UsrAlta = idUsuario,
                FAlta = DateTime.Now,
                FModi = new DateTime(1900, 1, 1),
                FBaja = new DateTime(1900, 1, 1)
            };

            _context.DRI_CatMonoVigencias.Add(nuevaVigencia);
            _context.SaveChanges();

            return new VigenciaDto
            {
                IdVigencia = nuevaVigencia.IdVigencia,
                Categoria = nuevaVigencia.Categoria,
                CantUCMs = nuevaVigencia.CantUCMs,
                PorcDescPC = nuevaVigencia.PorcDescPC,
                FVigenciaDesde = nuevaVigencia.FVigenciaDesde,
                FVigenciaHasta = nuevaVigencia.FVigenciaHasta,
                ValorUCM = nuevaVigencia.ValorUCM,
                CantidadCuotas = nuevaVigencia.CantidadCuotas
            };
        }

        public void ModificarVigencia(int idVigencia, VigenciaDto dto, int idUsuario)
        {
            var vigencia = _context.DRI_CatMonoVigencias.Find(idVigencia);
            if (vigencia == null)
                throw new Exception("Vigencia no encontrada");

            vigencia.Categoria = dto.Categoria;
            vigencia.CantUCMs = dto.CantUCMs;
            vigencia.PorcDescPC = dto.PorcDescPC;
            vigencia.UsrModi = idUsuario;
            vigencia.FModi = DateTime.Now;

            _context.SaveChanges();
        }

        public void EliminarVigencia(int idVigencia, int idUsuario)
        {
            var vigencia = _context.DRI_CatMonoVigencias.Find(idVigencia);
            if (vigencia == null)
                throw new Exception("Vigencia no encontrada");

            vigencia.UsrBaja = idUsuario;
            vigencia.FBaja = DateTime.Now;

            _context.SaveChanges();
        }

        private decimal ObtenerValorUCM(int cuenta, DateTime fecha)
        {
            return _rutinasService.ObtenerValorUCM(fecha);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DReI.BackWeb.Models.Dto
{
    /// <summary>
    /// DTO para Configuración Tributaria
    /// </summary>
    public class ConfiguracionTributariaDto
    {
        public int IdConfig { get; set; }
        public int IdTipoCalc { get; set; }
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        public decimal Alicuota { get; set; }
        public decimal CantUCM { get; set; }
    }

    /// <summary>
    /// DTO para Configuración de Actividades
    /// </summary>
    public class ConfiguracionActividadDto
    {
        public int IdRegConfig { get; set; }
        public int IdConfig { get; set; }
        public int NroNomenclador { get; set; }
        public int CodActividad { get; set; }
    }

    /// <summary>
    /// DTO para Configuración del Sistema
    /// </summary>
    public class ConfiguracionSistemaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorDec { get; set; }
        public int ValorInt { get; set; }
        public DateTime ValorDT { get; set; }
        public string ValorStr { get; set; }
        public DateTime FVigenciaDesde { get; set; }
        public DateTime FVigenciaHasta { get; set; }
    }
}
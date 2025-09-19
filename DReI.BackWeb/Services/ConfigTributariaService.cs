using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DReI.BackWeb.Data;
using DReI.BackWeb.Models.Entities;

namespace DReI.BackWeb.Services
{
    public class ConfigTributariaService
    {
        private int ObtenerUsuarioActual()
        {
            return 1; 
        }

        public void CrearConfiguracion(ConfiguracionTributaria config, int usuarioActual)
        {
            config.ValoresPorDefecto(usuarioActual); 

            using (var context = new ApplicationDbContext())
            {
                context.DRI_Configuracion.Add(config);
                context.SaveChanges();
            }
        }

        public List<ConfiguracionTributaria> ObtenerListaActivas()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.DRI_Configuracion
                    .Where(c => c.FBaja.Year == 1900)
                    .OrderByDescending(c => c.FDesde.Year)
                    .ToList(); 
            }
        }

        public ConfiguracionTributaria ObtenerPorId(int idConfig)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.DRI_Configuracion
                    .Where(c => c.FBaja.Year == 1900)
                    .FirstOrDefault(c => c.IdConfig == idConfig);
            }
        }
    }
}
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
        // Método para obtener el usuario actual (debes implementarlo según tu lógica)
        private int ObtenerUsuarioActual()
        {
            // ⚠️ Mejor: pásalo como parámetro desde el controlador
            return 1; // ← ¡Cambia esto según tu lógica real!
        }

        public void CrearConfiguracion(ConfiguracionTributaria config, int usuarioActual)
        {
            config.ValoresPorDefecto(usuarioActual); 

            using (var context = new DbContext())
            {
                context.DRI_Configuracion.Add(config);
                context.SaveChanges();
            }
        }

        // 🔁 Migrado desde DRI_ConfiguracionBL.Lista()
        public List<ConfiguracionTributaria> ObtenerListaActivas()
        {
            using (var context = new DbContext())
            {
                return context.DRI_Configuracion
                    .Where(c => c.FBaja.Year == 1900)
                    .OrderByDescending(c => c.FDesde.Year)
                    .ToList(); // ← Ejecuta la consulta aquí, no devuelve IQueryable
            }
        }

        // 🔁 Migrado desde DRI_ConfiguracionBL.Obtener()
        public ConfiguracionTributaria ObtenerPorId(int idConfig)
        {
            using (var context = new DbContext())
            {
                return context.DRI_Configuracion
                    .Where(c => c.FBaja.Year == 1900)
                    .FirstOrDefault(c => c.IdConfig == idConfig);
            }
        }
    }
}
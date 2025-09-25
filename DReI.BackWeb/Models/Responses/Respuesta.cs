using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DReI.BackWeb.Models.Responses
{
    /// <summary>
    /// Representa una respuesta estandarizada de la API.
    /// </summary>
    public class Respuesta
    {
        private readonly DateTime _fCreacion = DateTime.Now;

        /// <summary>
        /// Cantidad de errores en la lista.
        /// </summary>
        public int CantidadDeErrores => Error.Count;

        /// <summary>
        /// Lista de errores.
        /// </summary>
        public List<object> Error { get; } = new List<object>();

        /// <summary>
        /// Datos del resultado.
        /// </summary>
        public object Resultado { get; set; }

        /// <summary>
        /// Token (si aplica).
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Tiempo de ejecución desde la creación hasta la serialización.
        /// </summary>
        public string TiempoDeEjecucion => (DateTime.Now - _fCreacion).TotalMilliseconds.ToString("F2") + " ms";

        /// <summary>
        /// Log de procesos.
        /// </summary>
        public virtual List<string> Log { get; } = new List<string>();

        /// <summary>
        /// Log de consultas de Entity Framework.
        /// </summary>
        public virtual List<string> EntityFrameworkLog { get; } = new List<string>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public Respuesta()
        {
            _fCreacion = DateTime.Now;
        }

        /// <summary>
        /// Agrega un mensaje de error.
        /// </summary>
        public void AgregarMensajeDeError(string mensaje)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                Error.Add(mensaje);
            }
        }

        /// <summary>
        /// Agrega una lista de mensajes de error.
        /// </summary>
        public void AgregarMensajeDeError(List<string> mensajes)
        {
            foreach (string mensaje in mensajes)
            {
                AgregarMensajeDeError(mensaje);
            }
        }

        /// <summary>
        /// Serializa la respuesta a JSON.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
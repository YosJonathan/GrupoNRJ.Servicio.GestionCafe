// <copyright file="Bitacoras.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Utilidades
{
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Text.Json;
    using Microsoft.Extensions.Configuration;

    public class Bitacoras
    {
        private readonly EjecutarSP ejecutarSP;

        public Bitacoras(IConfiguration configuration)
        {
            this.ejecutarSP = new EjecutarSP(configuration, true);
        }

        /// <summary>
        /// Convierte un objeto genérico a JSON con formato.
        /// </summary>
        /// <typeparam name="T">Tipo del objeto</typeparam>
        /// <param name="objeto">Objeto a serializar</param>
        /// <returns>Cadena JSON</returns>
        public static string ConvertirAJson<T>(T objeto)
        {
            try
            {
                return JsonSerializer.Serialize(objeto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
            catch (Exception ex)
            {
                // En caso de error en la serialización, devolvemos un mensaje simple
                return $"Error al convertir a JSON: {ex.Message}";
            }
        }

        /// <summary>
        /// Guarda un mensaje de error en la consola incluyendo el nombre del método que llamó.
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        /// <param name="modelos">Modelo.</param>
        /// <param name="nombreMetodo">Nombre del método que llamó (se obtiene automáticamente si no se pasa)</param>
        public void GuardarError<T>(string mensaje, T modelos,
            [CallerMemberName] string nombreMetodo = "")
        {
            string cadena = $"Metodo: {nombreMetodo} - Excepción: {mensaje} - Parametros: {modelos}";
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "Descripcion", cadena }
                };

                this.ejecutarSP.ExecuteNonQuery("SP_AgregarError", parametros);
            }
            catch (Exception)
            {
            }
        }
    }
}

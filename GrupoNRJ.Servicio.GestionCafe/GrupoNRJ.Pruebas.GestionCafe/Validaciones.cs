// <copyright file="Validaciones.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Pruebas.GestionCafe
{
    /// <summary>
    /// Clase de validaciones.
    /// </summary>
    public class Validaciones
    {
        /// <summary>
        /// Valida codigo correcto y no tener mensajes de error.
        /// </summary>
        /// <param name="codigo">Codigo</param>
        /// <returns>Codigo correcto.</returns>
        internal static bool ValidarCodigo(int codigo, int valorEsperado = 0)
        {
            return codigo == valorEsperado;
        }

        internal static Dictionary<string, string> GenerarConfiguraciones()
        {
            var configuracion = new Dictionary<string, string>
            {
                { "ConnectionStrings:Configuracion", "Server=localhost;Database=Configuracion;Trusted_Connection=True;TrustServerCertificate=True;" },
                { "ConnectionStrings:DefaultConnection", "Server=localhost;Database=GestionCafe;Trusted_Connection=True;TrustServerCertificate=True;" }
            };
            return configuracion;
        }
    }
}

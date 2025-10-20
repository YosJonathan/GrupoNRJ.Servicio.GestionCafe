// <copyright file="Validaciones.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Pruebas.GestionCafe
{
    public class Validaciones
    {
        internal static bool ValidarCodigoYMensaje(int codigo, string mensaje)
        {
            return codigo == 0 && string.IsNullOrEmpty(mensaje);
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

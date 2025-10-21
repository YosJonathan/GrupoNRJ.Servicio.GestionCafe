// <copyright file="CafeFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

using GrupoNRJ.Servicio.GestionCafe.Abstract_Factory;

namespace GrupoNRJ.Servicio.GestionCafe.Factory
{
    public static class CafeFactory
    {
        public static ICafe CrearCafe(string tipo, string nivelTueste, IConfiguration configuration)
        {
            tipo = tipo.ToLower();
            nivelTueste = nivelTueste.ToLower();

            if (tipo == "arabica")
            {
                switch (nivelTueste)
                {
                    case "claro": return new CafeArabicaClaro(configuration);
                    case "medio": return new CafeArabicaMedio(configuration);
                    case "oscuro": return new CafeArabicaOscuro(configuration);
                }
            }
            else if (tipo == "robusta")
            {
                switch (nivelTueste)
                {
                    case "claro": return new CafeRobustaClaro(configuration);
                    case "medio": return new CafeRobustaMedio(configuration);
                    case "oscuro": return new CafeRobustaOscuro(configuration);
                }
            }
            else if (tipo == "blends")
            {
                switch (nivelTueste)
                {
                    case "claro": return new CafeBlendsClaro(configuration);
                    case "medio": return new CafeBlendsMedio(configuration);
                    case "oscuro": return new CafeBlendsOscuro(configuration);
                }
            }

            throw new ArgumentException("Tipo o nivel de tueste inválido.");
        }
    }
}

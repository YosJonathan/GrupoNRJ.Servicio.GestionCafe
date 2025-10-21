// <copyright file="CafeBlendsMedio.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Clase para cafe blend de medio.
    /// </summary>
    public class CafeBlendsMedio : ICafe
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CafeBlendsMedio"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuracion.</param>
        public CafeBlendsMedio(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Creación de nuevo lote.
        /// </summary>
        /// <param name="cantidad">Cantidad de producto.</param>
        /// <returns>Confirmación de creación.</returns>
        public RespuestaBase<bool> CreaLote(decimal cantidad)
        {
            var gestor = GestorDePlanificacion.GetInstance(this.configuration);
            RespuestaBase<bool> respuesta = gestor.CrearLote(cantidad, "BLENDS", "MEDIO");
            return respuesta;
        }
    }
}

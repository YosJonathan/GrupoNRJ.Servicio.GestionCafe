// <copyright file="CafeRobustaMedio.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;

    /// <summary>
    /// Clase para cafe robusta de medio.
    /// </summary>
    public class CafeRobustaMedio : ICafe
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CafeRobustaMedio"/> class.
        /// </summary>
        /// <param name="configuration">Objeto configuración.</param>
        public CafeRobustaMedio(IConfiguration configuration)
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
            RespuestaBase<bool> respuesta = gestor.CrearLote(cantidad, "ROBUSTA", "MEDIO");
            return respuesta;
        }
    }
}

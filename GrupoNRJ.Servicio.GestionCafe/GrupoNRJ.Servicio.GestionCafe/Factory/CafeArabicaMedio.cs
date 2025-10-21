// <copyright file="CafeArabicaMedio.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;

    /// <summary>
    /// Clase de cafe arabico tipo medio.
    /// </summary>
    public class CafeArabicaMedio : ICafe
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CafeArabicaMedio"/> class.
        /// </summary>
        /// <param name="configuration">Objeto configuracion.</param>
        public CafeArabicaMedio(IConfiguration configuration)
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
            RespuestaBase<bool> respuesta = gestor.CrearLote(cantidad, "ARÁBICA", "MEDIO");
            return respuesta;
        }
    }
}

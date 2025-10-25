// <copyright file="ServicioInventario.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Observer
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Servicio de inventario para alertas.
    /// </summary>
    public class ServicioInventario : ISubject
    {
        /// <summary>
        /// Objeto de configuración.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Lista de observadores.
        /// </summary>
        private readonly List<IObserver> observadores = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicioInventario"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        public ServicioInventario(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Agregar nuevo observador.
        /// </summary>
        /// <param name="observer">Observador.</param>
        public void Attach(IObserver observer) => this.observadores.Add(observer);

        /// <summary>
        /// Eliminar observador.
        /// </summary>
        /// <param name="observer">Observador.</param>
        public void Detach(IObserver observer) => this.observadores.Remove(observer);

        /// <summary>
        /// Notificador con productos bajos.
        /// </summary>
        /// <param name="productosBajos">Productos bajos.</param>
        public void Notify(List<ObtenerAlertasRespuesta> productosBajos)
        {
            foreach (var obs in this.observadores)
            {
                obs.Update(productosBajos);
            }
        }

       /// <summary>
       /// Vefica los productos bajos.
       /// </summary>
       /// <returns>Productos bajos.</returns>
        public RespuestaBase<List<ObtenerAlertasRespuesta>> VerificarProductosBajos()
        {
            RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = new();

            var gestor = GestorDeInventario.GetInstance(this.configuration);
            respuesta = gestor.ObtenerAlertas();

            if (respuesta?.Datos?.Count > 0)
            {
                this.Notify(respuesta.Datos);
            }

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return respuesta;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}

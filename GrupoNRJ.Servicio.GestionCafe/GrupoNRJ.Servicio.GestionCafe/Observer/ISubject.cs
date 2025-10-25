// <copyright file="ISubject.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

using GrupoNRJ.Modelos.GestionCafe.Respuestas;

namespace GrupoNRJ.Servicio.GestionCafe.Observer
{
    /// <summary>
    /// Clase de sujeto.
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// Agregar nuevo observador.
        /// </summary>
        /// <param name="observer">Observador.</param>
        void Attach(IObserver observer);

        /// <summary>
        /// Eliminar observador.
        /// </summary>
        /// <param name="observer">Observador.</param>
        void Detach(IObserver observer);

        /// <summary>
        /// Notificador con productos bajos.
        /// </summary>
        /// <param name="productosBajos">Productos bajos.</param>
        void Notify(List<ObtenerAlertasRespuesta> productosBajos);
    }
}

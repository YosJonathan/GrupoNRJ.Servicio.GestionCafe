// <copyright file="NotificadorAPI.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Observer
{
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;

    /// <summary>
    /// Notificador de API.
    /// </summary>
    public class NotificadorAPI : IObserver
    {
        /// <summary>
        /// Metodo actualizar los productos.
        /// </summary>
        /// <param name="productosBajos">Productos bajos.</param>
        public void Update(List<ObtenerAlertasRespuesta> productosBajos)
        {
            foreach (var p in productosBajos)
            {
                p.Mensaje = $"Min: {p.CantidadMinima} - Existencias: {p.Existencias}";
            }
        }
    }
}

// <copyright file="ICafe.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory
{
    using GrupoNRJ.Modelos.GestionCafe;

    /// <summary>
    /// Interfaz de cafe.
    /// </summary>
    public interface ICafe
    {
        /// <summary>
        /// Creación de lote.
        /// </summary>
        /// <param name="cantidad">Nueva cantidad.</param>
        /// <returns>Respuesta de lote.</returns>
        RespuestaBase<bool> CreaLote(decimal cantidad);
    }
}

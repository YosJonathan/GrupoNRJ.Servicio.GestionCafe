// <copyright file="IFiltro.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    /// <summary>
    /// Interfaz de filtro.
    /// </summary>
    public interface IFiltro
    {
        /// <summary>
        /// Obtener la descripción de filtro.
        /// </summary>
        /// <returns>Descripción de filtro.</returns>
        string Descripcion();

        /// <summary>
        /// Obtener el código del filtro.
        /// </summary>
        /// <returns>Código de filtro.</returns>
        int Codigo();
    }
}

// <copyright file="IFabricaDeCombos.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    /// <summary>
    /// Interfaz para la fabrica de combos.
    /// </summary>
    public interface IFabricaDeCombos
    {
        /// <summary>
        /// Crear Cafe.
        /// </summary>
        /// <returns>Creación de cafe.</returns>
        ICafe CrearCafe();

        /// <summary>
        /// Crear taza.
        /// </summary>
        /// <returns>Creación de taza.</returns>
        ITasa CrearTaza();

        /// <summary>
        /// Creación de filtro.
        /// </summary>
        /// <returns>Confirmación de filtro.</returns>
        IFiltro CrearFiltro();
    }
}

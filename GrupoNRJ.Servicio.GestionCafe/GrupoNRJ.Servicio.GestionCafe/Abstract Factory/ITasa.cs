// <copyright file="ITasa.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    /// <summary>
    /// Interfaz de taza.
    /// </summary>
    public interface ITasa
    {
        /// <summary>
        /// Obtiene la descripcion de la taza.
        /// </summary>
        /// <returns>Descripción de la taza.</returns>
        string Descripcion();

        /// <summary>
        /// Otiene el código de la taza.
        /// </summary>
        /// <returns>Código de la taza.</returns>
        int Codigo();
    }
}

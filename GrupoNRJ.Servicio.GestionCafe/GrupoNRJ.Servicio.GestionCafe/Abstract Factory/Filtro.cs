// <copyright file="Filtro.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    /// <summary>
    /// Clase para filtro.
    /// </summary>
    public class Filtro : IFiltro
    {
        /// <summary>
        /// Objeto de solicitud de producto.
        /// </summary>
        private readonly ProductoSolicitud producto;

        /// <summary>
        /// Initializes a new instance of the <see cref="Filtro"/> class.
        /// </summary>
        /// <param name="producto">Solicitud de producto.</param>
        public Filtro(ProductoSolicitud producto)
        {
            this.producto = producto;
        }

        /// <summary>
        /// Obtener información de descripción.
        /// </summary>
        /// <returns>Descripción de producto.</returns>
        public string Descripcion() => this.producto.Descripcion;

        /// <summary>
        /// Obtener codigo de producto.
        /// </summary>
        /// <returns>Codigo de producto.</returns>
        public int Codigo() => this.producto.Codigo;
    }
}

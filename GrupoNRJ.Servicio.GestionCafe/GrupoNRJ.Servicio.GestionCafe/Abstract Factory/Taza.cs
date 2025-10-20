// <copyright file="Taza.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    /// <summary>
    /// Modelo de taza.
    /// </summary>
    public class Taza : ITasa
    {
        /// <summary>
        /// Solicitud de producto de tasa.
        /// </summary>
        private readonly ProductoSolicitud producto;

        /// <summary>
        /// Initializes a new instance of the <see cref="Taza"/> class.
        /// </summary>
        /// <param name="producto">Producto Taza.</param>
        public Taza(ProductoSolicitud producto)
        {
            this.producto = producto;
        }

        /// <summary>
        /// Obtiene la descripción de la taza.
        /// </summary>
        /// <returns>Descripción de la taza.</returns>
        public string Descripcion() => this.producto.Descripcion;

        /// <summary>
        /// Obtiene el código de la taza.
        /// </summary>
        /// <returns>Código de la taza.</returns>
        public int Codigo() => this.producto.Codigo;
    }
}

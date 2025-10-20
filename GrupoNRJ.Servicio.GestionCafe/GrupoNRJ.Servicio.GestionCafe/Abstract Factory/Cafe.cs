// <copyright file="Cafe.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    /// <summary>
    /// Clase de cafe.
    /// </summary>
    public class Cafe : ICafe
    {
        /// <summary>
        /// Solicitud de producto.
        /// </summary>
        private readonly ProductoSolicitud producto;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cafe"/> class.
        /// </summary>
        /// <param name="producto">Producto.</param>
        public Cafe(ProductoSolicitud producto)
        {
            this.producto = producto;
        }

        /// <summary>
        /// Obtener descripción.
        /// </summary>
        /// <returns>Descripción del producto.</returns>
        public string Descripcion() => this.producto.Descripcion;

        /// <summary>
        /// Obtener código.
        /// </summary>
        /// <returns>Código de producto.</returns>
        public int Codigo() => this.producto.Codigo;
    }
}

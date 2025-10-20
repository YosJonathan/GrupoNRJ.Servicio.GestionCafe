// <copyright file="IProductoFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory_Method
{
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    /// <summary>
    /// Interfaz de fabrica de productos.
    /// </summary>
    public interface IProductoFactory
    {
        /// <summary>
        /// Agregar un nuevo producto.
        /// </summary>
        /// <param name="solicitud">Solicitud de agregación.</param>
        /// <returns>Respuesta de adición.</returns>
        public AgregarProductoRespuesta AgregarProducto(AgregarProductoSolicitud solicitud);
    }
}

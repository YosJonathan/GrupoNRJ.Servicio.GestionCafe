// <copyright file="IProductoFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory_Method
{
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    public interface IProductoFactory
    {
        public AgregarProductoRespuesta AgregarProducto(AgregarProductoSolicitud solicitud);
    }
}

// <copyright file="Cafe.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    public class Cafe : ICafe
    {
        private readonly ProductoSolicitud producto;

        public Cafe(ProductoSolicitud producto) { this.producto = producto; }

        public string Descripcion() => this.producto.Descripcion;

        public int Codigo() => this.producto.Codigo;
    }
}

// <copyright file="Filtro.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    public class Filtro : IFiltro
    {
        private readonly ProductoSolicitud producto;

        public Filtro(ProductoSolicitud producto) { this.producto = producto; }

        public string Descripcion() => this.producto.Descripcion;

        public int Codigo() => this.producto.Codigo;
    }
}

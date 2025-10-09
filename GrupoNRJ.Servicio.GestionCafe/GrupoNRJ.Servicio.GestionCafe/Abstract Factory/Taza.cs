// <copyright file="Taza.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    public class Taza : ITasa
    {
        private readonly ProductoSolicitud producto;

        public Taza(ProductoSolicitud producto) { this.producto = producto; }

        public string Descripcion() => this.producto.Descripcion;

        public int Codigo() => this.producto.Codigo;
    }
}

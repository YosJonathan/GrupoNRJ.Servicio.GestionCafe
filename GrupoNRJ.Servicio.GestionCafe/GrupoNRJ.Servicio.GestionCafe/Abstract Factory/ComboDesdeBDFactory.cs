// <copyright file="ComboDesdeBDFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    public class ComboDesdeBDFactory : IFabricaDeCombos
    {
        private readonly ProductoSolicitud cafe;
        private readonly ProductoSolicitud taza;
        private readonly ProductoSolicitud filtro;

        public ComboDesdeBDFactory(ProductoSolicitud cafe, ProductoSolicitud taza, ProductoSolicitud filtro)
        {
            this.cafe = cafe;
            this.taza = taza;
            this.filtro = filtro;
        }

        public ICafe CrearCafe() => new Cafe(this.cafe);

        public ITasa CrearTaza() => new Taza(this.taza);

        public IFiltro CrearFiltro() => new Filtro(this.filtro);
    }

}

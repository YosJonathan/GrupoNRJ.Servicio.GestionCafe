// <copyright file="ComboDesdeBDFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;

    /// <summary>
    /// Combo desde la fabrica.
    /// </summary>
    public class ComboDesdeBDFactory : IFabricaDeCombos
    {
        private readonly ProductoSolicitud cafe;
        private readonly ProductoSolicitud taza;
        private readonly ProductoSolicitud filtro;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboDesdeBDFactory"/> class.
        /// </summary>
        /// <param name="cafe">Producto Cafe.</param>
        /// <param name="taza">Producto Taza.</param>
        /// <param name="filtro">Producto Filtro.</param>
        public ComboDesdeBDFactory(ProductoSolicitud cafe, ProductoSolicitud taza, ProductoSolicitud filtro)
        {
            this.cafe = cafe;
            this.taza = taza;
            this.filtro = filtro;
        }

        /// <summary>
        /// Crear cafe.
        /// </summary>
        /// <returns>Obtener cafe.</returns>
        public ICafe CrearCafe() => new Cafe(this.cafe);

        /// <summary>
        /// Crear Taza.
        /// </summary>
        /// <returns>Obtener info taza.</returns>
        public ITasa CrearTaza() => new Taza(this.taza);

        /// <summary>
        /// Crear Filtro.
        /// </summary>
        /// <returns>Obtrener filtro.</returns>
        public IFiltro CrearFiltro() => new Filtro(this.filtro);
    }
}

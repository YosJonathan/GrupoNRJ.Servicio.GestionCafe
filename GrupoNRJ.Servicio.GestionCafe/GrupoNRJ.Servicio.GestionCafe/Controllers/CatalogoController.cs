// <copyright file="CatalogoController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controlador de catalogo.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        /// <summary>
        /// Configuración.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogoController"/> class.
        /// </summary>
        /// <param name="configuration">Configuración.</param>
        public CatalogoController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Obtiene el catalogo de granos.
        /// </summary>
        /// <returns>Catalogo de granos.</returns>
        [HttpPost("obtenerGranos")]
        public IActionResult ObtenerGranos()
        {
            var gestor = GestorDeCatalogo.GetInstance(this.configuration);
            RespuestaBase<List<GranosRespuesta>> respuesta = gestor.ObtenerGranos();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Obtiene el catalogo de tipo de producto.
        /// </summary>
        /// <returns>Catalogo de tipo de producto.</returns>
        [HttpPost("obtenerTipoProducto")]
        public IActionResult ObtenerTipoProducto()
        {
            var gestor = GestorDeCatalogo.GetInstance(this.configuration);
            RespuestaBase<List<TipoProductoResponse>> respuesta = gestor.ObtenerTipoProducto();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Obtiene el catalogo de combo.
        /// </summary>
        /// <returns>Catalogo de combos.</returns>
        [HttpPost("obtenerCatalogoCombo")]
        public IActionResult ObtenerCatalogoCombo()
        {
            var gestor = GestorDeCatalogo.GetInstance(this.configuration);
            RespuestaBase<ListadoCatalogoProductosRespuesta> respuesta = gestor.ObtenerCatalogoCombo();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Obtiene el estado de planificacion.
        /// </summary>
        /// <returns>Catalogo el estado de planificacion.</returns>
        [HttpGet("obtenerEstado")]
        public IActionResult ObtenerEstado()
        {
            var gestor = GestorDeCatalogo.GetInstance(this.configuration);
            RespuestaBase<List<CatalogoRespuesta>> respuesta = gestor.ObtenerEstadoPlanificacion();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Obtiene el lote de planificacion.
        /// </summary>
        /// <returns>Catalogo el estado de planificacion.</returns>
        [HttpGet("obtenerLote")]
        public IActionResult ObtenerLote()
        {
            var gestor = GestorDeCatalogo.GetInstance(this.configuration);
            RespuestaBase<List<CatalogoRespuesta>> respuesta = gestor.ObtenerLotePlanificacion();
            return this.Ok(respuesta);
        }
    }
}

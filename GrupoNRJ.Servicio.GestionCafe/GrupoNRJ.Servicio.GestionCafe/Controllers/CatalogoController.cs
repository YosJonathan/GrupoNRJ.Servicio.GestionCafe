// <copyright file="CatalogoController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public CatalogoController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("obtenerGranos")]
        public IActionResult ObtenerGranos()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<GranosRespuesta>> respuesta = gestor.ObtenerGranos();
            return this.Ok(respuesta);
        }

        [HttpPost("obtenerTipoProducto")]
        public IActionResult ObtenerTipoProducto()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<TipoProductoResponse>> respuesta = gestor.ObtenerTipoProducto();
            return this.Ok(respuesta);
        }

        [HttpPost("obtenerCatalogoCombo")]
        public IActionResult obtenerCatalogoCombo()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<ListadoCatalogoProductosRespuesta> respuesta = gestor.ObtenerCatalogoCombo();
            return this.Ok(respuesta);
        }
    }
}

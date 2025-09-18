// <copyright file="InventarioController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public InventarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("agregarProducto")]
        public IActionResult AgregarProducto(AgregarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            AgregarProductoRespuesta respuesta = gestor.AgregarProducto(solicitud);
            return this.Ok(respuesta);
        }

        [HttpPost("eliminarProducto")]
        public IActionResult EliminarProducto(EliminarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            EliminarProductoRespuesta respuesta = gestor.EliminarProducto(solicitud);
            return this.Ok(respuesta);
        }

        [HttpPost("inventario")]
        public IActionResult ConsultarInvetario()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<ProductoRespuesta>> resultado = gestor.ConsultarInventario();
            return this.Ok(resultado);
        }

        [HttpPost("ModificarProducto")]
        public IActionResult ModificarProducto(ModificarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            ModificarProductoRespuesta resultado = gestor.ModificarProducto(solicitud);
            return this.Ok(resultado);
        }

        [HttpPost("ConsultarMovimientos")]
        public IActionResult ConsultarMovimientosProducto(ConsultarMovimientosProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<ConsultarMovimientosProductoRespuesta>> resultado = gestor.ConsultarMovimientosProducto(solicitud);
            return this.Ok(resultado);
        }

        [HttpPost("IngresarMovimiento")]
        public IActionResult IngresarMovimiento(AgregarMovimientoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            AgregarMovimientoRespuesta resultado = gestor.AgregarMovimiento(solicitud);
            return this.Ok(resultado);
        }

        [HttpPost("ObtenerAlertas")]
        public IActionResult ObtenerAlertas()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = gestor.ObtenerAlertas();
            return this.Ok(respuesta);
        }

        [HttpPost("ObtenerInfoProducto")]
        public IActionResult ObtenerInfoProducto(ObtenerInfoProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<ObtenerInfoProductoRespuesta> resultado = gestor.ObtenerInfoProducto(solicitud);
            return this.Ok(resultado);
        }
    }
}

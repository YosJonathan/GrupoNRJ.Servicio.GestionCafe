// <copyright file="InventarioController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Observer;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controlador de inventario.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        /// <summary>
        /// Objeto de configuración.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventarioController"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        public InventarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Agregar un nuevo producto.
        /// </summary>
        /// <param name="solicitud">Solicitud de creación.</param>
        /// <returns>Confirmación de creación.</returns>
        [HttpPost("agregarProducto")]
        public IActionResult AgregarProducto(AgregarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            AgregarProductoRespuesta respuesta = gestor.AgregarProducto(solicitud);
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Eliminación de producto.
        /// </summary>
        /// <param name="solicitud">solicitud de eliminación.</param>
        /// <returns>confirmación de eliminación.</returns>
        [HttpPost("eliminarProducto")]
        public IActionResult EliminarProducto(EliminarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            EliminarProductoRespuesta respuesta = gestor.EliminarProducto(solicitud);
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Consulta de inventario.
        /// </summary>
        /// <returns>Respuesta de consulta de inventario.</returns>
        [HttpPost("inventario")]
        public IActionResult ConsultarInvetario()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<ProductoRespuesta>> resultado = gestor.ConsultarInventario();
            return this.Ok(resultado);
        }

        /// <summary>
        /// Modificar el producto.
        /// </summary>
        /// <param name="solicitud">Solicitud de modificación.</param>
        /// <returns>Confirmación de producto.</returns>
        [HttpPost("ModificarProducto")]
        public IActionResult ModificarProducto(ModificarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            ModificarProductoRespuesta resultado = gestor.ModificarProducto(solicitud);
            return this.Ok(resultado);
        }

        /// <summary>
        /// Consulta los movimientos del producto.
        /// </summary>
        /// <param name="solicitud">Solicitud de consulta de movimientos.</param>
        /// <returns>Listado de movimientos.</returns>
        [HttpPost("ConsultarMovimientos")]
        public IActionResult ConsultarMovimientosProducto(ConsultarMovimientosProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<ConsultarMovimientosProductoRespuesta>> resultado = gestor.ConsultarMovimientosProducto(solicitud);
            return this.Ok(resultado);
        }

        /// <summary>
        /// Ingresar movimientos.
        /// </summary>
        /// <param name="solicitud">Solicitud de ingreso.</param>
        /// <returns>Confirmación de ingresos.</returns>
        [HttpPost("IngresarMovimiento")]
        public IActionResult IngresarMovimiento(AgregarMovimientoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            AgregarMovimientoRespuesta resultado = gestor.AgregarMovimiento(solicitud);
            return this.Ok(resultado);
        }

        /// <summary>
        /// Obtener las alertas.
        /// </summary>
        /// <returns>Alertas de umbreales.</returns>
        [HttpPost("ObtenerAlertas")]
        public IActionResult ObtenerAlertas()
        {

            RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = new();
            ServicioInventario servicio = new(this.configuration);
            servicio.Attach(new NotificadorAPI());
            servicio.Attach(new NotificadorConsola());
            respuesta = servicio.VerificarProductosBajos();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Obtener la información del producto.
        /// </summary>
        /// <param name="solicitud">Solicitud de información de producto.</param>
        /// <returns>Listado de información de producto.</returns>
        [HttpPost("ObtenerInfoProducto")]
        public IActionResult ObtenerInfoProducto(ObtenerInfoProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<ObtenerInfoProductoRespuesta> resultado = gestor.ObtenerInfoProducto(solicitud);
            return this.Ok(resultado);
        }
    }
}

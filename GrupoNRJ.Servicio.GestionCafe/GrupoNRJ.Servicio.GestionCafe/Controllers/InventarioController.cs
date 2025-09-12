using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
using GrupoNRJ.Servicio.GestionCafe.Singleton;
using Microsoft.AspNetCore.Mvc;

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InventarioController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("agregarProducto")]
        public IActionResult AgregarProducto(AgregarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            AgregarProductoRespuesta respuesta = gestor.AgregarProducto(solicitud);
            return Ok(respuesta);
        }

        [HttpPost("eliminarProducto")]
        public IActionResult EliminarProducto(EliminarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            EliminarProductoRespuesta respuesta = gestor.EliminarProducto(solicitud);
            return Ok(respuesta);
        }

        [HttpPost("inventario")]
        public IActionResult ConsultarInvetario()
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            RespuestaBase<List<ProductoRespuesta>> resultado = gestor.ConsultarInventario();
            return Ok(resultado);
        }

        [HttpPost("ModificarProducto")]
        public IActionResult ModificarProducto(ModificarProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            ModificarProductoRespuesta resultado = gestor.ModificarProducto(solicitud);
            return Ok(resultado);
        }

        [HttpPost("ConsultarMovimientos")]
        public IActionResult ConsultarMovimientosProducto(ConsultarMovimientosProductoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            RespuestaBase<List<ConsultarMovimientosProductoRespuesta>> resultado = gestor.ConsultarMovimientosProducto(solicitud);
            return Ok(resultado);
        }

        [HttpPost("IngresarMovimiento")]
        public IActionResult IngresarMovimiento(AgregarMovimientoSolicitud solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            AgregarMovimientoRespuesta resultado = gestor.AgregarMovimiento(solicitud);
            return Ok(resultado);
        }

        [HttpPost("ObtenerAlertas")]
        public IActionResult ObtenerAlertas()
        {
            RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = new ();
            var gestor = GestorDeInventario.GetInstance(_configuration);
            respuesta = gestor.ObtenerAlertas();
            return this.Ok(respuesta);
        }
    }
}

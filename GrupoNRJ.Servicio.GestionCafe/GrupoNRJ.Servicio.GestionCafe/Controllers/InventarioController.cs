using Microsoft.AspNetCore.Mvc;

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Obtener()
        {
            var inventario = GestorDeInventario.Instancia.ObtenerInventario();
            return Ok(inventario);
        }

        [HttpPost("agregar")]
        public IActionResult AgregarProducto(int idProducto, int cantidad)
        {
            GestorDeInventario.Instancia.AgregarProducto(idProducto, cantidad);
            return Ok($"Se agregaron {cantidad} unidades del producto {idProducto}");
        }

        [HttpPost("retirar")]
        public IActionResult RetirarProducto(int idProducto, int cantidad)
        {
            GestorDeInventario.Instancia.RetirarProducto(idProducto, cantidad);
            return Ok($"Se retiraron {cantidad} unidades del producto {idProducto}");
        }
    }
}

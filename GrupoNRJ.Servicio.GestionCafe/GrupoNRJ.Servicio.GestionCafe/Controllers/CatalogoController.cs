using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
using GrupoNRJ.Servicio.GestionCafe.Singleton;
using Microsoft.AspNetCore.Mvc;

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CatalogoController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        [HttpPost("obtenerGranos")]
        public IActionResult ObtenerGranos()
        {
            var gestor = GestorDeInventario.GetInstance(_configuration);
            RespuestaBase<List<GranosRespuesta>> respuesta = gestor.ObtenerGranos();
            return Ok(respuesta);
        }
    }
}

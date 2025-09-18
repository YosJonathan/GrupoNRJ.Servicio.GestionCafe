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

        [HttpPost("obtenerNivelTostado")]
        public IActionResult ObtenerNivelTostado()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<NivelTostadoRespuesta>> respuesta = gestor.ObtenerNivelTostado();
            return this.Ok(respuesta);
        }
    }
}

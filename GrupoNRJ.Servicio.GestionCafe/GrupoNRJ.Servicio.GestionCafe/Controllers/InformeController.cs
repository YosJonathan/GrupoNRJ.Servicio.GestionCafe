// <copyright file="InformeController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Servicio.GestionCafe.Facade;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controlador para informes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InformeController : Controller
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="InformeController"/> class.
        /// </summary>
        /// <param name="configuration">Configuracion.</param>
        public InformeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Obtener el avance de los lotes.
        /// </summary>
        /// <returns>Respuesta de lotes.</returns>
        [HttpGet("reportes/avanceLotes")]
        public ActionResult GetAvanceLotes()
        {
            FacadeDeProduccion facadeDeProduccion = new(this.configuration);
            RespuestaBase<List<ObtenerLotesEnProcesoRespuesta>> respuesta = facadeDeProduccion.GenerarReporteAvanceLotes();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Obtener producción de área.
        /// </summary>
        /// <returns>Respuesta de producción de área.</returns>
        [HttpGet("reportes/produccionArea")]
        public ActionResult GetProduccionArea()
        {
            FacadeDeProduccion facadeDeProduccion = new(this.configuration);
            RespuestaBase<List<ObtenerProduccionPorAreaRespuesta>> respuesta = facadeDeProduccion.GenerarReporteProduccionPorArea();
            return this.Ok(respuesta);
        }
    }
}

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
        [HttpGet("reportes/ObtenerReportes")]
        public ActionResult GetAvanceLotes()
        {
            FacadeDeProduccion facadeDeProduccion = new(this.configuration);
            RespuestaBase<GeneracionReportesRespuesta> respuesta = facadeDeProduccion.GeneracionReportes();
            return this.Ok(respuesta);
        }
    }
}

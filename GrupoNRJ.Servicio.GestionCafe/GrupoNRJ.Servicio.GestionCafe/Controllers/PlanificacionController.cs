// <copyright file="PlanificacionController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PlanificacionController : ControllerBase
    {
        /// <summary>
        /// Configuración.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlanificacionController"/> class.
        /// </summary>
        /// <param name="sp">Ejecutar Sp.</param>
        /// <param name="configuration">Objeto de configuración.</param>
        public PlanificacionController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Obtiene los estados de cafe y lotes.
        /// </summary>
        /// <returns>Estado de lotes.</returns>
        [HttpGet("estadoLotes")]
        public ActionResult GetEstadoLotes()
        {
            var gestor = GestorDePlanificacion.GetInstance(this.configuration);
            RespuestaBase<List<ObtenerEstadoLotesRespuesta>> respuesta = gestor.ObtenerEstadoLotes();

            return this.Ok(respuesta);
        }

        /// <summary>
        /// Muestra la planificación de los lotes de café.
        /// </summary>
        /// <returns>Obtener planificación.</returns>
        [HttpGet("planificacion")]
        public ActionResult GetPlanificacion()
        {
            var gestor = GestorDePlanificacion.GetInstance(this.configuration);
            RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = gestor.ObtenerPlanificacion();

            return this.Ok(respuesta);
        }

        /// <summary>
        /// Muestra la planificación de los lotes.
        /// </summary>
        /// <param name="solicitud">Solicitud de planificación.</param>
        /// <returns>Información de planificación./returns>
        [HttpGet("planificacion/id")]
        public ActionResult GetPlanificacionLote([FromQuery] ObtenerPlanificacionLoteSolicitud solicitud)
        {
            var gestor = GestorDePlanificacion.GetInstance(this.configuration);
            RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = gestor.ObtenerLotesPlanificacion(solicitud);

            return this.Ok(respuesta);
        }

        /// <summary>
        /// Endpoint para una nueva planificación.
        /// </summary>
        /// <param name="solicitud">Solicitud de planificación.</param>
        /// <returns>Respuesta de ingreso de planificación.</returns>
        [HttpPost("planificacion/nueva")]
        public ActionResult PostCrearPlanificacion([FromBody] PlanificacionSolicitud solicitud)
        {
            var gestor = GestorDePlanificacion.GetInstance(this.configuration);
            RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = gestor.NuevaPlanificacion(solicitud);

            return this.Ok(respuesta);
        }

        /// <summary>
        /// Actualizar o modificar una nueva planificación.
        /// </summary>
        /// <param name="solicitud">Solicitud para actualizar.</param>
        /// <returns>Respuesta para actualizar.</returns>
        [HttpPost("planificacion/actualizar")]
        public ActionResult PostActualizarPlanificacion([FromBody] PlanificacionSolicitud solicitud)
        {
            var gestor = GestorDePlanificacion.GetInstance(this.configuration);
            RespuestaBase<List<PlanificacionSolicitud>> respuesta = gestor.ActualizarPlanificacion(solicitud);

            return this.Ok(respuesta);
        }
    }
}
// <copyright file="ComboController.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Abstract_Factory;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controlador de combos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        /// <summary>
        /// Objeto de configuración.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboController"/> class.
        /// </summary>
        /// <param name="configuration">Configuración de aplicación.</param>
        public ComboController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Agregar un combo.
        /// </summary>
        /// <param name="solicitud">Solicitud de combo.</param>
        /// <returns>Confirmación de agregar combo.</returns>
        [HttpPost("AgregarCombo")]
        public IActionResult AgregarCombo(AgregarComboSolicitud solicitud)
        {
            AgregarCombosRespuestas respuesta = new();
            IFabricaDeCombos fabrica = new ComboDesdeBDFactory(solicitud.Cafe, solicitud.Tasa, solicitud.Filtro);
            Combo combo = new Combo(fabrica);

            respuesta = combo.CreandoCombo(solicitud.Nombre, this.configuration);
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Listado de combos.
        /// </summary>
        /// <returns>Respuesta de listado de combos.</returns>
        [HttpPost("ListaCombos")]
        public IActionResult ListaCombos()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<CombosResponse>> respuesta = gestor.ObtenerListadoCombos();
            return this.Ok(respuesta);
        }

        /// <summary>
        /// Eliminar combo.
        /// </summary>
        /// <param name="solicitud">Solicitud de eliminación de combo.</param>
        /// <returns>Confirmación de eliminación.</returns>
        [HttpPost("eliminarCombo")]
        public IActionResult EliminarCombo(EliminarCombo solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            EliminarComboRespuesta respuesta = gestor.EliminarCombo(solicitud);
            return this.Ok(respuesta);
        }
    }
}

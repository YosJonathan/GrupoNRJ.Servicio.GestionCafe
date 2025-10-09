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

    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ComboController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost("AgregarCombo")]
        public IActionResult AgregarCombo(AgregarComboSolicitud solicitud)
        {
            AgregarCombosRespuestas respuesta = new();
            IFabricaDeCombos fabrica = new ComboDesdeBDFactory(solicitud.Cafe, solicitud.Tasa, solicitud.Filtro);
            Combo combo = new Combo(fabrica);

            respuesta = combo.CreandoCombo(solicitud.Nombre, this.configuration);
            return this.Ok(respuesta);
        }


        [HttpPost("ListaCombos")]
        public IActionResult ListaCombos()
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            RespuestaBase<List<CombosResponse>> respuesta = gestor.ObtenerListadoCombos();
            return this.Ok(respuesta);
        }

        [HttpPost("eliminarCombo")]
        public IActionResult EliminarCombo(EliminarCombo solicitud)
        {
            var gestor = GestorDeInventario.GetInstance(this.configuration);
            EliminarComboRespuesta respuesta = gestor.EliminarCombo(solicitud);
            return this.Ok(respuesta);
        }

    }
}

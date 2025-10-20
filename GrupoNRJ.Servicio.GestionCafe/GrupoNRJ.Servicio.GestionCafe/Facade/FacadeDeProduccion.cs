// <copyright file="FacadeDeProduccion.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Facade
{
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Servicio.GestionCafe.Singleton;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    /// <summary>
    /// Clase de producción de facade.
    /// </summary>
    public class FacadeDeProduccion
    {
        /// <summary>
        /// Objeto de configuración.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Objeto de bitacoras de error.
        /// </summary>
        private readonly Bitacoras bitacora;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeDeProduccion"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        public FacadeDeProduccion(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.bitacora = new Bitacoras(configuration);
        }

        /// <summary>
        /// Generación de reportes de lotes y areas.
        /// </summary>
        /// <returns>Listado de reportes.</returns>
        public RespuestaBase<GeneracionReportesRespuesta> GeneracionReportes()
        {
            RespuestaBase<GeneracionReportesRespuesta> respuesta = new();
            try
            {
                var gestor = GestorDeReportes.GetInstance(this.configuration);
                respuesta.Datos = new();
                respuesta.Datos.ReportesPorLotes = gestor.ObtenerLotesEnProceso();
                respuesta.Datos.ReportesPorAreas = gestor.ObtenerProduccionPorArea();
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
                this.bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }
    }
}

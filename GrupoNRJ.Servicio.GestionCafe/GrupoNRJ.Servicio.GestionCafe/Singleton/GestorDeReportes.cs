// <copyright file="GestorDeReportes.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Singleton
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    public class GestorDeReportes
    {
        private static GestorDeReportes? instancia;
        private readonly EjecutarSP ejecutarSP;
        private readonly Bitacoras bitacora;
#pragma warning disable SA1204 // Static elements must appear before instance elements
        private static readonly object Mlock = new();
#pragma warning restore SA1204 // Static elements must appear before instance elements

        /// <summary>
        /// Initializes a new instance of the <see cref="GestorDeReportes"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        public GestorDeReportes(IConfiguration configuration)
        {
            this.ejecutarSP = new EjecutarSP(configuration);
            this.bitacora = new Bitacoras(configuration);
        }

        /// <summary>
        /// Obtener la instancia.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        /// <returns>Objeto de clase.</returns>
        public static GestorDeReportes GetInstance(IConfiguration configuration)
        {
            if (instancia == null)
            {
                lock (Mlock)
                {
                    instancia ??= new GestorDeReportes(configuration);
                }
            }

            return instancia;
        }

        /// <summary>
        /// Obtener Lotes en proceso.
        /// </summary>
        /// <returns>Lotes en estado pendiente.</returns>
        internal List<ObtenerLotesEnProcesoRespuesta> ObtenerLotesEnProceso()
        {
            List<ObtenerLotesEnProcesoRespuesta> respuesta = new();
            DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_REPORTE_AVANCE_LOTE");

            respuesta = new();
            foreach (DataRow row in dt.Rows)
            {
                respuesta.Add(new ObtenerLotesEnProcesoRespuesta
                {
                    IdLote = Convert.ToInt32(row["IDLOTE"]),
                    TipoGrano = row["TIPOGRANO"].ToString() ?? string.Empty,
                    TipoTueste = row["TIPOTUESTE"].ToString() ?? string.Empty,
                    EstadoActual = Convert.ToInt32(row["ESTADO_ACTUAL"]),
                    FechaUltimoCambio = Convert.ToDateTime(row["FECHA_ULTIMO_CAMBIO"]),
                    Situacion = row["SITUACION"].ToString() ?? string.Empty
                });
            }

            return respuesta;
        }

        /// <summary>
        /// Obtener producción por area.
        /// </summary>
        /// <returns>Producción por area.</returns>
        internal List<ObtenerProduccionPorAreaRespuesta> ObtenerProduccionPorArea()
        {
            List<ObtenerProduccionPorAreaRespuesta> respuesta = new();
            DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_REPORTE_PRODUCCION_AREA");

            respuesta = new();
            foreach (DataRow row in dt.Rows)
            {
                respuesta.Add(new ObtenerProduccionPorAreaRespuesta
                {
                    IdLote = Convert.ToInt32(row["IDLOTE"]),
                    Estado = Convert.ToInt32(row["ESTADO"]),
                    Area = row["AREA"].ToString() ?? string.Empty
                });
            }

            return respuesta;
        }
    }
}

// <copyright file="GestorDePlanificacion.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Singleton
{
    using System.Data;
    using Azure.Core;
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    public class GestorDePlanificacion
    {
        private static readonly object Mlock = new();
        private static GestorDePlanificacion? instancia;
        private readonly EjecutarSP ejecutarSP;
        private readonly Bitacoras bitacora;

        /// <summary>
        /// Initializes a new instance of the <see cref="GestorDePlanificacion"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        public GestorDePlanificacion(IConfiguration configuration)
        {
            this.ejecutarSP = new EjecutarSP(configuration);
            this.bitacora = new Bitacoras(configuration);
        }

        /// <summary>
        /// Obtener la instancia.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        /// <returns>Objeto de clase.</returns>
        public static GestorDePlanificacion GetInstance(IConfiguration configuration)
        {
            if (instancia == null)
            {
                lock (Mlock)
                {
                    instancia ??= new GestorDePlanificacion(configuration);
                }
            }

            return instancia;
        }

        /// <summary>
        /// Obtener los estados de lotes.
        /// </summary>
        /// <returns>Estados de lote.</returns>
        public RespuestaBase<List<ObtenerEstadoLotesRespuesta>> ObtenerEstadoLotes()
        {
            RespuestaBase<List<ObtenerEstadoLotesRespuesta>> respuesta = new();
            try
            {
                DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_S_DATOS_LOTE");

                // Convertir DataTable en lista de objetos anónimos
                respuesta.Datos = new();
                foreach (DataRow row in dt.Rows)
                {
                    respuesta.Datos.Add(new ObtenerEstadoLotesRespuesta
                    {
                        IdLote = int.Parse(row["NO_LOTE"].ToString() ?? "0"),
                        Cantidad = double.Parse(row["KILOGRAMOS"].ToString() ?? "0"),
                        TipoGrano = row["TIPO_GRANO"].ToString() ?? string.Empty,
                        TipoTueste = row["TUESTE"].ToString() ?? string.Empty,
                        FechaInicio = DateTime.Parse(row["FECHA_LLEGADA"].ToString() ?? default(DateTime).ToString()),
                        FechaFin = DateTime.Parse(row["FECHA_FINALIZADA"].ToString() ?? default(DateTime).ToString()),
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
                this.bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }

        /// <summary>
        /// Obtiene la planificación.
        /// </summary>
        /// <returns>Planificación.</returns>
        public RespuestaBase<List<ObtenerPlanificacionRespuesta>> ObtenerPlanificacion()
        {
            RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = new();
            try
            {
                DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_S_DATOS_PLANI_LOTE");

                respuesta.Datos = new();
                foreach (DataRow row in dt.Rows)
                {
                    respuesta.Datos.Add(new ObtenerPlanificacionRespuesta
                    {
                        IdPlanificacion = int.Parse(row["NO_PLANI"].ToString() ?? "0"),
                        IdLote = int.Parse(row["NO_LOTE"].ToString() ?? "0"),
                        Estado = row["ESTADO"].ToString() ?? string.Empty,
                        FechaInicio = DateTime.Parse(row["FECHA_INICIO_ESTIMADA"].ToString() ?? default(DateTime).ToString()),
                        FechaFin = DateTime.Parse(row["FECHA_FIN_ESTIMADA"].ToString() ?? default(DateTime).ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
                this.bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }

        /// <summary>
        /// Obtiene lotes de planficación.
        /// </summary>
        /// <param name="solicitud">Solicitud de planificación.</param>
        /// <returns>Lista de lotes planificación.</returns>
        public RespuestaBase<List<ObtenerPlanificacionRespuesta>> ObtenerLotesPlanificacion(ObtenerPlanificacionLoteSolicitud solicitud)
        {
            RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = new();
            try
            {
                var parametros = new Dictionary<string, object>
                {
                { "@P_IDPLANIFICACION", solicitud.IdPlanificacionLote }
                };

                DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_S_DATOS_PLANI_LOTE_B", parametros);

                respuesta.Datos = new();

                var info_estado = new List<object>();
                foreach (DataRow row in dt.Rows)
                {
                    respuesta.Datos.Add(new ObtenerPlanificacionRespuesta
                    {
                        IdPlanificacion = int.Parse(row["NO_PLANI"].ToString() ?? "0"),
                        IdLote = int.Parse(row["NO_LOTE"].ToString() ?? "0"),
                        Estado = row["ESTADO"].ToString() ?? string.Empty,
                        FechaInicio = DateTime.Parse(row["FECHA_INICIO_ESTIMADA"].ToString() ?? default(DateTime).ToString()),
                        FechaFin = DateTime.Parse(row["FECHA_FIN_ESTIMADA"].ToString() ?? default(DateTime).ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
                this.bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }

        /// <summary>
        /// Crear una nueva planfiicación.
        /// </summary>
        /// <param name="solicitud">Solicitud de creación.</param>
        /// <returns>Respuesta de creación.</returns>
        public RespuestaBase<List<ObtenerPlanificacionRespuesta>> NuevaPlanificacion(PlanificacionSolicitud solicitud)
        {
            RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = new();

            try
            {
                var parametros = new Dictionary<string, object>
            {
                { "@IDLOTE", solicitud.IdLote },
                { "@IDESTADO", solicitud.IdEstado },
                { "@FECHAESTIMADA", solicitud.FechaEstimada ?? default(DateTime) },
                { "@FECHAFINESTIMADA", solicitud.FechaFinEstimada ?? default(DateTime) }
            };

                DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_I_NUEVA_PLANIFICACION", parametros);

                respuesta.Datos = new();
                foreach (DataRow row in dt.Rows)
                {
                    respuesta.Datos.Add(new ObtenerPlanificacionRespuesta
                    {
                        IdPlanificacion = int.Parse(row["IDPLANIFICACION"].ToString() ?? "0"),
                        IdLote = int.Parse(row["IDLOTE"].ToString() ?? "0"),
                        Estado = row["IDESTADO"].ToString() ?? string.Empty,
                        FechaInicio = DateTime.Parse(row["FECHAESTIMADA"].ToString() ?? default(DateTime).ToString()),
                        FechaFin = DateTime.Parse(row["FECHAFINESTIMADA"].ToString() ?? default(DateTime).ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
                this.bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }

        public RespuestaBase<List<PlanificacionSolicitud>> ActualizarPlanificacion(PlanificacionSolicitud solicitud)
        {
            RespuestaBase<List<PlanificacionSolicitud>> respuesta = new();
            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@P_IDPLANIFICACION", solicitud.IdPlani },
                    { "@P_IDLOTE", solicitud.IdLote },
                    { "@P_IDESTADO", solicitud.IdEstado },
                    { "@P_FECHAESTIMADA", solicitud.FechaEstimada ?? DateTime.MinValue },
                    { "@P_FECHAFINESTIMADA", solicitud.FechaFinEstimada ?? DateTime.MinValue }
                };

                DataTable dt = this.ejecutarSP.ExecuteStoredProcedure("SP_U_PLANIFICACION", parametros);

                respuesta.Datos = new();
                foreach (DataRow row in dt.Rows)
                {
                    respuesta.Datos.Add(new PlanificacionSolicitud
                    {
                        IdPlani = Convert.ToInt32(row["IDPLANIFICACION"]),
                        IdLote = Convert.ToInt32(row["IDLOTE"]),
                        IdEstado = Convert.ToInt32(row["IDESTADO"]),
                        FechaEstimada = row["FECHAESTIMADA"] == DBNull.Value ? null : Convert.ToDateTime(row["FECHAESTIMADA"]),
                        FechaFinEstimada = row["FECHAFINESTIMADA"] == DBNull.Value ? null : Convert.ToDateTime(row["FECHAFINESTIMADA"])
                    });
                }
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

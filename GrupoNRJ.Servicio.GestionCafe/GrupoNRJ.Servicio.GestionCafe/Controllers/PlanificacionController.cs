using GrupoNRJ.Servicio.GestionCafe;
using GrupoNRJ.Servicio.GestionCafe.Factory;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanificacionController : ControllerBase
    {
        private readonly EjecutarSP sp;
        public PlanificacionController(EjecutarSP sp)
        {
            this.sp = sp;
        }
        /// <summary>
        /// MUESTRA EL ESTADO DE LOS LOTES DE CAFE
        /// </summary>
        /// <returns></returns>
        [HttpGet("estadoLotes")]
        public ActionResult getEstadoLotes()
        {

            DataTable dt = sp.ExecuteStoredProcedure("SP_S_DATOS_LOTE");

            // Convertir DataTable en lista de objetos anónimos
            var info_estado = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                info_estado.Add(new
                {
                    idLote = row["NO_LOTE"],
                    cantidad = row["KILOGRAMOS"],
                    tipoGrano = row["TIPO_GRANO"],
                    tipoTueste = row["TUESTE"],
                    fechaIn = row["FECHA_LLEGADA"],
                    fechaOut = row["FECHA_FINALIZADA"],
                });
            }

            return Ok(info_estado);

        }
        /// <summary>
        /// MUESTRA LA PLANIFICACION DE TODOS LOS LOTES DE CAFE
        /// </summary>
        /// <returns></returns>
        [HttpGet("planificacion")]
        public ActionResult getPlanificacion()
        {

            DataTable dt = sp.ExecuteStoredProcedure("SP_S_DATOS_PLANI_LOTE");

            var info_estado = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                info_estado.Add(new
                {
                    idPlani = row["NO_PLANI"],
                    idLote = row["NO_LOTE"],
                    estado = row["ESTADO"],
                    fechaInicio = row["FECHA_INICIO_ESTIMADA"],
                    fechaFin = row["FECHA_FIN_ESTIMADA"]
                });
            }

            return Ok(info_estado);

        }
        /// <summary>
        /// MUESTRA LA PLANIFICACION DE UN LOTE DE CAFE ESPECIFICO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("planificacion/id")]
        public ActionResult getPlanificacionLote([FromQuery] int id)
        {

            var parametros = new Dictionary<string, object>
            {
                { "@P_IDPLANIFICACION", id }
            };

            DataTable dt = sp.ExecuteStoredProcedure("SP_S_DATOS_PLANI_LOTE_B", parametros);

            // Convertir DataTable en lista de objetos anónimos
            var info_estado = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                info_estado.Add(new
                {
                    idPlaini = row["NO_PLANI"],
                    idLote = row["NO_LOTE"],
                    estado = row["ESTADO"],
                    fechaInicio = row["FECHA_INICIO_ESTIMADA"],
                    fechaFin = row["FECHA_FIN_ESTIMADA"]
                });
            }

            return Ok(info_estado);

        }

        [HttpPost("planificacion/nueva")]
        public ActionResult postCrearPlanificacion([FromBody] dtoPlanificacion request)
        {
            var parametros = new Dictionary<string, object>
            {
                { "@IDLOTE", request.IdLote },
                { "@IDESTADO", request.IdEstado },
                { "@FECHAESTIMADA", request.FechaEstimada },
                { "@FECHAFINESTIMADA", request.FechaFinEstimada }
            };

            DataTable dt = sp.ExecuteStoredProcedure("SP_I_NUEVA_PLANIFICACION", parametros);


            var resultado = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                resultado.Add(new
                {
                    IdPlanificacion = row["IDPLANIFICACION"],
                    IdLote = row["IDLOTE"],
                    Estado = row["IDESTADO"],
                    FechaEstimada = row["FECHAESTIMADA"],
                    FechaFinEstimada = row["FECHAFINESTIMADA"]
                });
            }

            return Ok(resultado);
        }


        [HttpPost("planificacion/actualizar")]
        public ActionResult postActualizarPlanificacion([FromBody] dtoPlanificacion request)
        {
            try
            {
                var parametros = new Dictionary<string, object>
                {
                    { "@P_IDPLANIFICACION", request.IdPlani },
                    { "@P_IDLOTE", request.IdLote },
                    { "@P_IDESTADO", request.IdEstado },
                    { "@P_FECHAESTIMADA", request.FechaEstimada == DateTime.MinValue ? DBNull.Value : request.FechaEstimada },
                    { "@P_FECHAFINESTIMADA", request.FechaFinEstimada == DateTime.MinValue ? DBNull.Value : request.FechaFinEstimada }
                };

                DataTable dt = sp.ExecuteStoredProcedure("SP_U_PLANIFICACION", parametros);

                var resultado = new List<dtoPlanificacion>();
                foreach (DataRow row in dt.Rows)
                {
                    resultado.Add(new dtoPlanificacion
                    {
                        IdPlani = Convert.ToInt32(row["IDPLANIFICACION"]),
                        IdLote = Convert.ToInt32(row["IDLOTE"]),
                        IdEstado = Convert.ToInt32(row["IDESTADO"]),
                        FechaEstimada = row["FECHAESTIMADA"] == DBNull.Value ? null : Convert.ToDateTime(row["FECHAESTIMADA"]),
                        FechaFinEstimada = row["FECHAFINESTIMADA"] == DBNull.Value ? null : Convert.ToDateTime(row["FECHAFINESTIMADA"])
                    });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("crearLote")]
        public ActionResult CrearLote([FromBody] dtoInfoCafe request)
        {

            try
            {
                ICafe cafe = CafeFactory.CrearCafe(request.tipoGrano, request.tipoTueste, sp);
                cafe.creaLote(request.cantidad);

                return Ok(new
                {
                    success = true,
                    message = $"Lote de café {request.tipoGrano} {request.tipoTueste} creado correctamente."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Error en los datos del café: {ex.Message}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error inesperado al crear el lote: {ex.Message}"
                });
            }

        }



    }
}
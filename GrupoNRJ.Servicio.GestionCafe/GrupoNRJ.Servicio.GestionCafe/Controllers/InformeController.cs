using Microsoft.AspNetCore.Mvc;
using System.Data;
using GrupoNRJ.Servicio.GestionCafe.Composite;

namespace GrupoNRJ.Servicio.GestionCafe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformeController : Controller
    {

        private readonly EjecutarSP sp;
        public InformeController(EjecutarSP sp)
        {
            this.sp = sp;
        }

        [HttpGet("reportes/avanceLotes")]
        public ActionResult getAvanceLotes()
        {
            DataTable dt = sp.ExecuteStoredProcedure("SP_REPORTE_AVANCE_LOTE");

            var reporteLote = new List<Object>();

            foreach (DataRow row in dt.Rows)
            {
                reporteLote.Add(new
                {
                    idLote = Convert.ToInt32(row["IDLOTE"]),
                    tipoGrano = row["TIPOGRANO"].ToString(),
                    tipoTueste = row["TIPOTUESTE"].ToString(),
                    estadoActual = Convert.ToInt32(row["ESTADO_ACTUAL"]),
                    fechaUltimoCambio = Convert.ToDateTime(row["FECHA_ULTIMO_CAMBIO"]),
                    situacion = row["SITUACION"].ToString()
                });
            }

            return Ok(reporteLote);
        }

        [HttpGet("reportes/produccionArea")]
        public ActionResult getProduccionArea()
        {
            DataTable dt = sp.ExecuteStoredProcedure("SP_REPORTE_PRODUCCION_AREA");


            var produccionArea = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                produccionArea.Add(new
                {
                    idLote = Convert.ToInt32(row["IDLOTE"]),
                    estado = Convert.ToInt32(row["ESTADO"]),
                    area = row["AREA"].ToString()                    
                });
            }


            return Ok(produccionArea);
        }


        [HttpGet("procesos/{id}")]
        public ActionResult GetProcesoJerarquia(int id, [FromServices] ProcesoService service)
        {
            var proceso = service.CargarJerarquia(id);

            proceso.Mostrar();
            double avance = proceso.CalcularAvance();

            return Ok(new
            {
                Proceso = proceso.Nombre,
                AvanceTotal = avance
            });
        }

    }
}

using System.Data;

namespace GrupoNRJ.Servicio.GestionCafe.Composite
{
    public class ProcesoService
    {
        private readonly EjecutarSP _sp;

        public ProcesoService(EjecutarSP sp)
        {
            _sp = sp;
        }

        public ProcesoProduccion CargarJerarquia(int idProceso)
        {
            var parametros = new Dictionary<string, object>
        {
            { "@P_IDPROCESO", idProceso }
        };

            DataTable dt = _sp.ExecuteStoredProcedure("SP_S_PROCESO_JERARQUIA", parametros);

            var procesos = new Dictionary<int, ProcesoProduccion>();

            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["IDPROCESO"]);
                string nombre = row["NOMBRE"].ToString()!;
                int? padre = row["IDPADRE"] == DBNull.Value ? null : Convert.ToInt32(row["IDPADRE"]);

                ProcesoProduccion nodo;

                // Ejemplo: si tiene hijos -> Proceso, si no -> Tarea
                if (dt.AsEnumerable().Any(r => r["IDPADRE"] != DBNull.Value && Convert.ToInt32(r["IDPADRE"]) == id))
                    nodo = new Proceso { Id = id, Nombre = nombre };
                else
                    nodo = new Tarea { Id = id, Nombre = nombre, Avance = 0 };

                procesos[id] = nodo;

                if (padre.HasValue && procesos.ContainsKey(padre.Value))
                {
                    ((Proceso)procesos[padre.Value]).Agregar(nodo);
                }
            }

            return procesos[idProceso];
        }
    }

}

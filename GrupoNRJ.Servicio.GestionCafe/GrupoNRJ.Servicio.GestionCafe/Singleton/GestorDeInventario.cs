using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
using GrupoNRJ.Servicio.GestionCafe.Utilidades;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Data;

namespace GrupoNRJ.Servicio.GestionCafe.Singleton
{
    public class GestorDeInventario
    {
        private static GestorDeInventario? _instancia;
        private static readonly object _lock = new();
        private readonly EjecutarSP ejecutarSP;
        private readonly Dictionary<int, int> _inventario;

        // 🔒 Constructor privado
        private GestorDeInventario(IConfiguration configuration)
        {
            _inventario = new Dictionary<int, int>();
            ejecutarSP = new EjecutarSP(configuration);
        }

        // 🔑 Acceso Singleton
        public static GestorDeInventario GetInstance(IConfiguration configuration)
        {
            if (_instancia == null)
            {
                lock (_lock)
                {
                    if (_instancia == null)
                    {
                        _instancia = new GestorDeInventario(configuration);
                    }
                }
            }
            return _instancia;
        }

        // 📌 Métodos principales
        public AgregarProductoRespuesta AgregarProducto(AgregarProductoSolicitud solicitud)
        {
            AgregarProductoRespuesta respuesta = new AgregarProductoRespuesta();
            try
            {
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros = new Dictionary<string, object>
                {
                    {"Nombre", solicitud.Nombre },
                    {"Cantidad", solicitud.Cantidad },
                    {"GranoId", solicitud.IdGrano },
                    {"ValorMinimo", solicitud.ValorMinimo }
                };

                respuesta.RegistroIngresadoCorrectamente = ejecutarSP.ExecuteNonQuery("SP_GuardarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroIngresadoCorrectamente ? "Registro ingresado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.ToString();
                Bitacoras.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }

        public ModificarProductoRespuesta ModificarProducto(ModificarProductoSolicitud solicitud)
        {
            ModificarProductoRespuesta respuesta = new ModificarProductoRespuesta();
            try
            {
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros = new Dictionary<string, object>
                {
                    {"Nombre", solicitud.Nombre },
                    {"IdProducto", solicitud.IdProducto },
                    {"GranoId", solicitud.GranoId },
                    {"ValorMinimo", solicitud.ValorMinimo }
                };

                respuesta.RegistroModificadoExitosamente = ejecutarSP.ExecuteNonQuery("SP_ModificarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroModificadoExitosamente ? "Registro ingresado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                Bitacoras.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }

        public EliminarProductoRespuesta EliminarProducto(EliminarProductoSolicitud solicitud)
        {
            EliminarProductoRespuesta respuesta = new EliminarProductoRespuesta();
            try
            {

                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros = new Dictionary<string, object>
                {
                    {"IdProducto", solicitud.IdProducto }
                };

                respuesta.RegistroEliminadoExitosamente = ejecutarSP.ExecuteNonQuery("SP_EliminarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroEliminadoExitosamente ? "Registro ingresado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                Bitacoras.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }

        public RespuestaBase<List<ProductoRespuesta>> ConsultarInventario()
        {
            RespuestaBase<List<ProductoRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                using var _ = resultado = ejecutarSP.ExecuteStoredProcedure("SP_InventarioProductos", parametros);

                if (resultado != null)
                {
                    List<ProductoRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        ProductoRespuesta registro = new ProductoRespuesta();
                        registro.IdProducto = int.Parse(dr["ProductoID"].ToString() ?? "0");
                        registro.Nombre = dr["NombreProducto"].ToString() ?? string.Empty;
                        registro.Cantidad = double.Parse(dr["Cantidad"].ToString() ?? "0");
                        registro.Grano = dr["Grano"].ToString() ?? string.Empty;
                        lista.Add(registro);
                    }
                    respuesta.Datos = lista;
                }
            }
            catch (Exception ex)
            {
                Bitacoras.GuardarError(ex.ToString(), new { });
                respuesta.Mensaje = ex.ToString();
                respuesta.Codigo = 999;
            }
            return respuesta;
        }


        public RespuestaBase<List<ConsultarMovimientosProductoRespuesta>> ConsultarMovimientosProducto(ConsultarMovimientosProductoSolicitud solicitud)
        {
            RespuestaBase<List<ConsultarMovimientosProductoRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros = new Dictionary<string, object>
                {
                    {"Producto", solicitud.IdProducto }
                };

                using var _ = resultado = ejecutarSP.ExecuteStoredProcedure("SP_ConsultarMovimientosProducto", parametros);


                if (resultado != null)
                {
                    List<ConsultarMovimientosProductoRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        ConsultarMovimientosProductoRespuesta registro = new();
                        registro.IdProducto = int.Parse(dr["ProductoID"].ToString() ?? "0");
                        registro.NombreProducto = dr["NombreProducto"].ToString() ?? string.Empty;
                        registro.Cantidad = double.Parse(dr["Cantidad"].ToString() ?? "0");
                        registro.TipoMovimiento = dr["Tipo Movimiento"].ToString() ?? string.Empty;
                        registro.FechaMovimiento = DateTime.Parse(dr["FechaMovimiento"].ToString() ?? string.Empty);
                        lista.Add(registro);
                    }
                    respuesta.Datos = lista;
                }
            }
            catch (Exception ex)
            {
                Bitacoras.GuardarError(ex.ToString(), new { });
            }
            return respuesta;
        }
        public AgregarMovimientoRespuesta AgregarMovimiento(AgregarMovimientoSolicitud solicitud)
        {
            AgregarMovimientoRespuesta respuesta = new AgregarMovimientoRespuesta();
            try
            {  
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros = new Dictionary<string, object>
                {
                    {"IdProducto", solicitud.IdProducto },
                    {"Cantidad", solicitud.Cantidad },
                    {"TipoMovimiento", solicitud.TipoMovimiento }
                };

                resultado = ejecutarSP.ExecuteStoredProcedure("SP_MovimientoProducto", parametros);

                if (resultado !=null)
                {
                    DataRow dr = resultado.Rows[0];
                    respuesta.Codigo = int.Parse(dr["Codigo"].ToString()??"0");
                    respuesta.Mensaje = dr["Mensaje"].ToString()??string.Empty;
                
                }

                respuesta.Mensaje = respuesta.Codigo == 0 ? "Ocurrio un error inesperado": respuesta.Mensaje;
            }
            catch (Exception ex)
            {
                Bitacoras.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }


        public Dictionary<int, int> ObtenerInventario()
        {
            return _inventario;
        }

        internal RespuestaBase<List<ObtenerAlertasRespuesta>> ObtenerAlertas()
        {
            RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = new ();
            try
            {
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                using var _ = resultado = ejecutarSP.ExecuteStoredProcedure("SP_ObtenerAlertas", parametros);
                if (resultado != null)
                {
                    List<ObtenerAlertasRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new ObtenerAlertasRespuesta
                        {
                            AlertaStock = int.Parse(dr["AlertaEnStock"].ToString() ?? "")==1,
                            NombreProducto = dr["NombreProducto"].ToString() ?? string.Empty,
                            CantidadMinima = double.Parse(dr["CantidadMinima"].ToString() ?? "0"),
                            Existencias = double.Parse(dr["existencias"].ToString() ?? "0")
                        });
                    }
                    respuesta.Datos = lista;
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.ToString();
                respuesta.Codigo = 999;
                Bitacoras.GuardarError(ex.ToString(), new { });
            }
            return respuesta;
        }

        internal RespuestaBase<List<GranosRespuesta>> ObtenerGranos()
        {
            RespuestaBase<List<GranosRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new DataTable();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                using var _ = resultado = ejecutarSP.ExecuteStoredProcedure("SP_ObtenerGranos", parametros);
                if (resultado != null)
                {
                    List<GranosRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new GranosRespuesta
                        {
                            IdGranos = int.Parse(dr["GranoID"].ToString() ?? "0"),
                            Nombre = dr["Tipo"].ToString() ??string.Empty,
                        });
                    }
                    respuesta.Datos = lista;
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
                Bitacoras.GuardarError(ex.ToString(), new { });
            }
            return respuesta;
        }
    }

}

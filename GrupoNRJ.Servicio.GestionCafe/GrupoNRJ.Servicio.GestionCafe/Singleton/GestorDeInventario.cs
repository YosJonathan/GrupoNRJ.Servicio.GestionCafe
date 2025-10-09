// <copyright file="GestorDeInventario.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Singleton
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Factory_Method;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    public class GestorDeInventario
    {
        private static GestorDeInventario? instancia;
        private static readonly object mlock = new();
        private readonly EjecutarSP ejecutarSP;
        private readonly Bitacoras bitacora;
        private readonly Dictionary<int, int> inventario;

        // 🔒 Constructor privado
        private GestorDeInventario(IConfiguration configuration)
        {
            this.inventario = new Dictionary<int, int>();
            this.ejecutarSP = new EjecutarSP(configuration);
            this.bitacora = new Bitacoras(configuration);
        }

        // 🔑 Acceso Singleton
        public static GestorDeInventario GetInstance(IConfiguration configuration)
        {
            if (instancia == null)
            {
                lock (mlock)
                {
                    instancia ??= new GestorDeInventario(configuration);
                }
            }

            return instancia;
        }

        // 📌 Métodos principales
        public AgregarProductoRespuesta AgregarProducto(AgregarProductoSolicitud solicitud)
        {
            AgregarProductoRespuesta respuesta = new();
            IProductoFactory factory;
            switch (solicitud.Grano)
            {
                case "Arábica":
                    factory = new ArabicaFactory(this.ejecutarSP, this.bitacora);
                    break;
                case "Robusta":
                    factory = new RobustaFactory(this.ejecutarSP, this.bitacora);
                    break;
                case "Blend Especial":
                    factory = new BlendEspecialFactory(this.ejecutarSP, this.bitacora);
                    break;
                default:
                    respuesta.Mensaje = "Grano no reconocido";
                    return respuesta;
            }

            respuesta = factory.AgregarProducto(solicitud);

            return respuesta;
        }

        public ModificarProductoRespuesta ModificarProducto(ModificarProductoSolicitud solicitud)
        {
            ModificarProductoRespuesta respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "Nombre", solicitud.Nombre },
                    { "IdProducto", solicitud.IdProducto },
                    { "GranoId", solicitud.GranoId },
                    { "ValorMinimo", solicitud.ValorMinimo },
                };

                respuesta.RegistroModificadoExitosamente = this.ejecutarSP.ExecuteNonQuery("SP_ModificarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroModificadoExitosamente ? "Registro modificado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }

        public EliminarProductoRespuesta EliminarProducto(EliminarProductoSolicitud solicitud)
        {
            EliminarProductoRespuesta respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "IdProducto", solicitud.IdProducto }
                };

                respuesta.RegistroEliminadoExitosamente = this.ejecutarSP.ExecuteNonQuery("SP_EliminarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroEliminadoExitosamente ? "Registro ingresado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }

        public RespuestaBase<List<ProductoRespuesta>> ConsultarInventario()
        {
            RespuestaBase<List<ProductoRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_InventarioProductos", parametros);

                if (resultado != null)
                {
                    List<ProductoRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        ProductoRespuesta registro = new()
                        {
                            IdProducto = int.Parse(dr["ProductoID"].ToString() ?? "0"),
                            Nombre = dr["NombreProducto"].ToString() ?? string.Empty,
                            Cantidad = double.Parse(dr["Cantidad"].ToString() ?? "0"),
                            Grano = dr["Grano"].ToString() ?? string.Empty,
                            TipoProducto = dr["Tipo Producto"].ToString() ?? string.Empty,
                        };
                        lista.Add(registro);
                    }

                    respuesta.Datos = lista;
                }
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), new { });
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
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "Producto", solicitud.IdProducto }
                };

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ConsultarMovimientosProducto", parametros);

                if (resultado != null)
                {
                    List<ConsultarMovimientosProductoRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        ConsultarMovimientosProductoRespuesta registro = new()
                        {
                            IdProducto = int.Parse(dr["ProductoID"].ToString() ?? "0"),
                            NombreProducto = dr["NombreProducto"].ToString() ?? string.Empty,
                            Cantidad = double.Parse(dr["Cantidad"].ToString() ?? "0"),
                            TipoMovimiento = dr["Tipo Movimiento"].ToString() ?? string.Empty,
                            FechaMovimiento = DateTime.Parse(dr["FechaMovimiento"].ToString() ?? string.Empty)
                        };
                        lista.Add(registro);
                    }

                    respuesta.Datos = lista;
                }
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), new { });
                respuesta.Mensaje = ex.ToString();
                respuesta.Codigo = 999;
            }

            return respuesta;
        }

        public AgregarMovimientoRespuesta AgregarMovimiento(AgregarMovimientoSolicitud solicitud)
        {
            AgregarMovimientoRespuesta respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = [];
                parametros = new Dictionary<string, object>
                {
                    { "IdProducto", solicitud.IdProducto },
                    { "Cantidad", solicitud.Cantidad },
                    { "TipoMovimiento", solicitud.TipoMovimiento }
                };

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_MovimientoProducto", parametros);

                if (resultado != null)
                {
                    DataRow dr = resultado.Rows[0];
                    respuesta.Codigo = int.Parse(dr["Codigo"].ToString() ?? "0");
                    respuesta.Mensaje = dr["Mensaje"].ToString() ?? string.Empty;
                }

                respuesta.Mensaje = respuesta.Codigo == 0 ? "Ocurrio un error inesperado" : respuesta.Mensaje;
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }

        public Dictionary<int, int> ObtenerInventario()
        {
            return this.inventario;
        }

        internal RespuestaBase<List<ObtenerAlertasRespuesta>> ObtenerAlertas()
        {
            RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = [];

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerAlertas", parametros);
                if (resultado != null)
                {
                    List<ObtenerAlertasRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new ObtenerAlertasRespuesta
                        {
                            AlertaStock = int.Parse(dr["AlertaEnStock"].ToString() ?? string.Empty) == 1,
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
                this.bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }

        internal RespuestaBase<List<GranosRespuesta>> ObtenerGranos()
        {
            RespuestaBase<List<GranosRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = [];

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerGranos", parametros);
                if (resultado != null)
                {
                    List<GranosRespuesta> lista = [];
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new GranosRespuesta
                        {
                            IdGranos = int.Parse(dr["GranoID"].ToString() ?? "0"),
                            Nombre = dr["Tipo"].ToString() ?? string.Empty,
                        });
                    }

                    respuesta.Datos = lista;
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

        internal RespuestaBase<ObtenerInfoProductoRespuesta> ObtenerInfoProducto(ObtenerInfoProductoSolicitud solicitud)
        {
            RespuestaBase<ObtenerInfoProductoRespuesta> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "IdProducto", solicitud.IdProducto },
                };
                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerInfoProductos", parametros);
                if (resultado != null)
                {
                    ObtenerInfoProductoRespuesta infoProducto = new();
                    DataRow dr = resultado.Rows[0];

#pragma warning disable CS8604 // Posible argumento de referencia nulo
                    infoProducto.GranoId = int.Parse(!string.IsNullOrEmpty(dr["GranoID"].ToString()) ? dr["GranoID"].ToString() : "0");
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                    infoProducto.IdProducto = int.Parse(dr["ProductoID"].ToString() ?? "0");
                    infoProducto.ValorMinimo = double.Parse(dr["CantidadMinima"].ToString() ?? "0");
                    infoProducto.Nombre = dr["NombreProducto"].ToString() ?? string.Empty;
                    respuesta.Datos = infoProducto;
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
            }

            return respuesta;
        }

        internal RespuestaBase<List<TipoProductoResponse>> ObtenerTipoProducto()
        {
            RespuestaBase<List<TipoProductoResponse>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = [];

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerTipoProducto", parametros);
                if (resultado != null)
                {
                    List<TipoProductoResponse> lista = [];
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new TipoProductoResponse
                        {
                            IdTipoProducto = int.Parse(dr["IdTipoProducto"].ToString() ?? "0"),
                            Nombre = dr["Nombre"].ToString() ?? string.Empty,
                        });
                    }

                    respuesta.Datos = lista;
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

        internal RespuestaBase<ListadoCatalogoProductosRespuesta> ObtenerCatalogoCombo()
        {
            RespuestaBase<ListadoCatalogoProductosRespuesta> respuesta = new();

            try
            {
                respuesta.Datos = new();
                DataTable resultado = new();
                Dictionary<string, object> parametros = [];

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerProductosCombos", parametros);
                if (resultado != null)
                {
                    List<CatalogoProductoResponse> cafe =new(), tasa = new(), filtro = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        switch (dr["Tipo"].ToString())
                        {
                            case "1":
                                cafe.Add(
                                    new CatalogoProductoResponse
                                    {
                                        Codigo = int.Parse(dr["ProductoID"].ToString()),
                                        Nombre = dr["NombreProducto"].ToString(),
                                    }
                                    );
                                break;
                            case "2":
                                tasa.Add(
                                    new CatalogoProductoResponse
                                    {
                                        Codigo = int.Parse(dr["ProductoID"].ToString()),
                                        Nombre = dr["NombreProducto"].ToString(),
                                    }
                                    );
                                break;
                            case "3":
                                filtro.Add(
                                    new CatalogoProductoResponse
                                    {
                                        Codigo = int.Parse(dr["ProductoID"].ToString()),
                                        Nombre = dr["NombreProducto"].ToString(),
                                    }
                                    );
                                break;
                            default:
                                break;
                        }
                    }

                    respuesta.Datos.Cafe = cafe;
                    respuesta.Datos.Tasa = tasa;
                    respuesta.Datos.Filtros = filtro;
                }
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), new { });
                respuesta.Codigo = 999;
                respuesta.Mensaje = ex.ToString();
            }

            return respuesta;
        }

        internal RespuestaBase<List<CombosResponse>> ObtenerListadoCombos()
        {
            RespuestaBase<List<CombosResponse>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = [];

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ListadosCombos", parametros);
                if (resultado != null)
                {
                    List<CombosResponse> lista = [];
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new CombosResponse
                        {
                            IdCodigoCombo = int.Parse(dr["IdComboProducto"].ToString() ?? "0"),
                            Nombre = dr["Nombre"].ToString() ?? string.Empty,
                            Descripcion = dr["Descripcion"].ToString() ?? string.Empty,
                        });
                    }

                    respuesta.Datos = lista;
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

        internal EliminarComboRespuesta EliminarCombo(EliminarCombo solicitud)
        {
            EliminarComboRespuesta respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "IdCombo", solicitud.IdCombo }
                };

                respuesta.ComboEliminadoExitosamente = this.ejecutarSP.ExecuteNonQuery("SP_EliminarCombo", parametros);
                respuesta.Mensaje = respuesta.ComboEliminadoExitosamente ? "Combo eliminado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                this.bitacora.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }
    }
}

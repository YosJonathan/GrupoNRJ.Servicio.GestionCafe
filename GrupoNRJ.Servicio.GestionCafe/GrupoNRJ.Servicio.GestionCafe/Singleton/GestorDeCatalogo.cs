// <copyright file="GestorDeCatalogo.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Singleton
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    /// <summary>
    /// Gestor de catalogo.
    /// </summary>
    public class GestorDeCatalogo
    {
        private static readonly object Mlock = new();
        private static GestorDeCatalogo? instancia;
        private readonly EjecutarSP ejecutarSP;
        private readonly Bitacoras bitacora;

        /// <summary>
        /// Initializes a new instance of the <see cref="GestorDeCatalogo"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        public GestorDeCatalogo(IConfiguration configuration)
        {
            this.ejecutarSP = new EjecutarSP(configuration);
            this.bitacora = new Bitacoras(configuration);
        }

        /// <summary>
        /// Obtener la instancia.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        /// <returns>Objeto de clase.</returns>
        public static GestorDeCatalogo GetInstance(IConfiguration configuration)
        {
            if (instancia == null)
            {
                lock (Mlock)
                {
                    instancia ??= new GestorDeCatalogo(configuration);
                }
            }

            return instancia;
        }

        /// <summary>
        /// Obtener catalogo de granos.
        /// </summary>
        /// <returns>Listado de catalogos.</returns>
        public RespuestaBase<List<GranosRespuesta>> ObtenerGranos()
        {
            RespuestaBase<List<GranosRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerGranos", parametros);
                if (resultado != null)
                {
                    List<GranosRespuesta> lista = new();
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

        /// <summary>
        /// Obtener catalogo de tipo de producto.
        /// </summary>
        /// <returns>Listado de tipos de productos.</returns>
        public RespuestaBase<List<TipoProductoResponse>> ObtenerTipoProducto()
        {
            RespuestaBase<List<TipoProductoResponse>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerTipoProducto", parametros);
                if (resultado != null)
                {
                    List<TipoProductoResponse> lista = new();
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

        /// <summary>
        /// Obtener catalogos de combos.
        /// </summary>
        /// <returns>Listado para llenar combos.</returns>
        public RespuestaBase<ListadoCatalogoProductosRespuesta> ObtenerCatalogoCombo()
        {
            RespuestaBase<ListadoCatalogoProductosRespuesta> respuesta = new();

            try
            {
                respuesta.Datos = new();
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerProductosCombos", parametros);
                if (resultado != null)
                {
                    List<CatalogoProductoResponse> cafe = new(), tasa = new(), filtro = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        switch (dr["Tipo"].ToString())
                        {
                            case "1":
                                cafe.Add(
                                    new CatalogoProductoResponse
                                    {
                                        Codigo = int.Parse(dr["ProductoID"].ToString() ?? "0"),
                                        Nombre = dr["NombreProducto"].ToString() ?? string.Empty,
                                    });
                                break;
                            case "2":
                                tasa.Add(
                                    new CatalogoProductoResponse
                                    {
                                        Codigo = int.Parse(dr["ProductoID"].ToString() ?? "0"),
                                        Nombre = dr["NombreProducto"].ToString() ?? string.Empty,
                                    });
                                break;
                            case "3":
                                filtro.Add(
                                    new CatalogoProductoResponse
                                    {
                                        Codigo = int.Parse(dr["ProductoID"].ToString() ?? "0"),
                                        Nombre = dr["NombreProducto"].ToString() ?? string.Empty,
                                    });
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

        /// <summary>
        /// Obtener un estado de planificación.
        /// </summary>
        /// <returns>Estado de planificación.</returns>
        public RespuestaBase<List<CatalogoRespuesta>> ObtenerEstadoPlanificacion()
        {
            RespuestaBase<List<CatalogoRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerEstadoLote", parametros);
                if (resultado != null)
                {
                    List<CatalogoRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new CatalogoRespuesta
                        {
                            IdCatalogo = int.Parse(dr["ID"].ToString() ?? "0"),
                            Nombre = dr["NOMBRE"].ToString() ?? string.Empty,
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

        /// <summary>
        /// Obtener los lotes de planificación.
        /// </summary>
        /// <returns>Lotes de planificación.</returns>
        public RespuestaBase<List<CatalogoRespuesta>> ObtenerLotePlanificacion()
        {
            RespuestaBase<List<CatalogoRespuesta>> respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();

                resultado = this.ejecutarSP.ExecuteStoredProcedure("SP_ObtenerLotes", parametros);
                if (resultado != null)
                {
                    List<CatalogoRespuesta> lista = new();
                    foreach (DataRow dr in resultado.Rows)
                    {
                        lista.Add(new CatalogoRespuesta
                        {
                            IdCatalogo = int.Parse(dr["ID"].ToString() ?? "0"),
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
    }
}

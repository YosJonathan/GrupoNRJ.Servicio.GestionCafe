// <copyright file="ArabicaFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory_Method
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    /// <summary>
    /// Fabrica de productos arabicos.
    /// </summary>
    public class ArabicaFactory : IProductoFactory
    {
        private readonly EjecutarSP ejecutarSP;
        private readonly Bitacoras bitacora;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArabicaFactory"/> class.
        /// </summary>
        /// <param name="ejecutarSP">Ejecutar SPs.</param>
        /// <param name="bitacora">Bitacoras.</param>
        public ArabicaFactory(EjecutarSP ejecutarSP, Bitacoras bitacora)
        {
            this.ejecutarSP = ejecutarSP;
            this.bitacora = bitacora;
        }

        /// <summary>
        /// Agregar nuevo producto.
        /// </summary>
        /// <param name="solicitud">Solicitud de agregar nuevo producto.</param>
        /// <returns>Respuesta de agregar.</returns>
        public AgregarProductoRespuesta AgregarProducto(AgregarProductoSolicitud solicitud)
        {
            AgregarProductoRespuesta respuesta = new();
            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "Nombre", solicitud.Nombre },
                    { "Cantidad", solicitud.Cantidad },
                    { "ValorMinimo", solicitud.ValorMinimo },
                    { "TipoProducto", solicitud.TipoProducto },
                    { "GranoId", solicitud.IdGrano },
                };

                respuesta.RegistroIngresadoCorrectamente = this.ejecutarSP.ExecuteNonQuery("SP_GuardarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroIngresadoCorrectamente ? "Registro ingresado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.ToString();
                this.bitacora.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }
    }
}

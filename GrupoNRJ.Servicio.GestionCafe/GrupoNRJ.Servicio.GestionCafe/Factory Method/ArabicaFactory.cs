// <copyright file="ArabicaFactory.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Factory_Method
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    public class ArabicaFactory : IProductoFactory
    {
        private readonly EjecutarSP ejecutarSP;

        public ArabicaFactory(EjecutarSP ejecutarSP)
        {
            this.ejecutarSP = ejecutarSP;
        }

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
                    { "GranoId", solicitud.IdGrano },
                    { "ValorMinimo", solicitud.ValorMinimo },
                    { "NivelTostado", solicitud.NivelTostado }
                };

                respuesta.RegistroIngresadoCorrectamente = this.ejecutarSP.ExecuteNonQuery("SP_GuardarProducto", parametros);
                respuesta.Mensaje = respuesta.RegistroIngresadoCorrectamente ? "Registro ingresado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.ToString();
                Bitacoras.GuardarError(ex.ToString(), solicitud);
            }

            return respuesta;
        }
    }
}

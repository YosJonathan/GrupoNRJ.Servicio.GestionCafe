// <copyright file="Combo.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    using System.Data;
    using GrupoNRJ.Modelos.GestionCafe.Respuestas;
    using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
    using GrupoNRJ.Servicio.GestionCafe.Utilidades;

    public class Combo
    {
        private readonly ICafe cafe;
        private readonly ITasa taza;
        private readonly IFiltro filtro;

        public Combo(IFabricaDeCombos fabrica)
        {
            // El cliente no sabe "cómo" se crean los objetos,
            // solo usa la fábrica que le entregan
            this.cafe = fabrica.CrearCafe();
            this.taza = fabrica.CrearTaza();
            this.filtro = fabrica.CrearFiltro();
        }

        public AgregarCombosRespuestas CreandoCombo(string nombre, IConfiguration configuration)
        {
            AgregarCombosRespuestas respuesta = new();
            EjecutarSP ejecutarSP = new(configuration);
            Bitacoras bitacora = new(configuration);

            try
            {
                DataTable resultado = new();
                Dictionary<string, object> parametros = new();
                parametros = new Dictionary<string, object>
                {
                    { "Nombre", nombre },
                    { "Cafe", this.cafe.Codigo() },
                    { "Tasa", this.taza.Codigo() },
                    { "Filtro", this.filtro.Codigo() },
                };

                respuesta.ComboAgregado = ejecutarSP.ExecuteNonQuery("SP_AgregarCombo", parametros);
                respuesta.Mensaje = respuesta.ComboAgregado ? "Combo agregado exitosamente" : "Ocurrio un error";
            }
            catch (Exception ex)
            {
                bitacora.GuardarError(ex.ToString(), new { });
            }

            return respuesta;
        }
    }

}

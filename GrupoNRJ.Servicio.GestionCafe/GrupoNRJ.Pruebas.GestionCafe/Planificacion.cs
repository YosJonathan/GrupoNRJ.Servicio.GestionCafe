// <copyright file="Planificacion.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
using GrupoNRJ.Servicio.GestionCafe.Factory;
using GrupoNRJ.Servicio.GestionCafe.Singleton;
using Microsoft.Extensions.Configuration;

namespace GrupoNRJ.Pruebas.GestionCafe;

public class Planificacion
{
    private readonly IConfiguration configuration;

    public Planificacion()
    {
        // Construimos IConfiguration con esos valores
#pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
        this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(Validaciones.GenerarConfiguraciones())
            .Build();
#pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
    }

    [Fact]
    public void CrearLote()
    {
        RespuestaBase<bool> respuesta = new();
        try
        {
            ICafe cafe = CafeFactory.CrearCafe("arabica", "claro", this.configuration);
            respuesta = cafe.CreaLote(2);
        }
        catch (ArgumentException ex)
        {
            respuesta.Codigo = 999;
            respuesta.Mensaje = ex.ToString();
        }

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void NuevaPlanificacion()
    {
        PlanificacionSolicitud solicitud = new PlanificacionSolicitud
        {
            IdPlani = 1,
            IdLote = 1,
            IdEstado = 1,
            FechaEstimada = DateTime.Now,
            FechaFinEstimada = DateTime.Now.AddDays(1),
        };
        var gestor = GestorDePlanificacion.GetInstance(this.configuration);
        RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = gestor.NuevaPlanificacion(solicitud);
        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ActualizarPlanificacion()
    {
        PlanificacionSolicitud solicitud = new PlanificacionSolicitud
        {
            IdPlani = 1,
            IdLote = 1,
            IdEstado = 1,
            FechaEstimada = DateTime.Now,
            FechaFinEstimada = DateTime.Now.AddDays(1),
        };
        var gestor = GestorDePlanificacion.GetInstance(this.configuration);
        RespuestaBase<List<PlanificacionSolicitud>> respuesta = gestor.ActualizarPlanificacion(solicitud);
        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ObtenerEstadoLotes()
    {
        var gestor = GestorDePlanificacion.GetInstance(this.configuration);
        RespuestaBase<List<ObtenerEstadoLotesRespuesta>> respuesta = gestor.ObtenerEstadoLotes();
        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ObtenerPlanificacion()
    {
        var gestor = GestorDePlanificacion.GetInstance(this.configuration);
        RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = gestor.ObtenerPlanificacion();
        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ObtenerLotesPlanificacion()
    {
        ObtenerPlanificacionLoteSolicitud solicitud = new ObtenerPlanificacionLoteSolicitud
        {
            IdPlanificacionLote = 1,
        };
        var gestor = GestorDePlanificacion.GetInstance(this.configuration);
        RespuestaBase<List<ObtenerPlanificacionRespuesta>> respuesta = gestor.ObtenerLotesPlanificacion(solicitud);
        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }
}

// <copyright file="Inventario.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
using GrupoNRJ.Servicio.GestionCafe.Facade;
using GrupoNRJ.Servicio.GestionCafe.Singleton;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GrupoNRJ.Pruebas.GestionCafe;

public class Inventario
{
    private readonly IConfiguration configuration;

    public Inventario()
    {
        // Construimos IConfiguration con esos valores
#pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
        this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(Validaciones.GenerarConfiguraciones())
            .Build();
#pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
    }

    [Fact]
    public void AgregarProducto()
    {
        AgregarProductoSolicitud solicitud = new AgregarProductoSolicitud
        {
            Nombre = "Nombre prueba 1",
            Cantidad = 15,
            IdGrano = 1,
            ValorMinimo = 10,
            Grano = "Arábica",
            TipoProducto = 1
        };

        var gestor = GestorDeInventario.GetInstance(this.configuration);
        AgregarProductoRespuesta respuesta = gestor.AgregarProducto(solicitud);

        Assert.NotNull(respuesta);
        Assert.True(respuesta.RegistroIngresadoCorrectamente);
    }

    [Fact]
    public void ConsultarInvetario()
    {
        var gestor = GestorDeInventario.GetInstance(this.configuration);
        RespuestaBase<List<ProductoRespuesta>> respuesta = gestor.ConsultarInventario();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ConsultarMovimientosProducto()
    {
        ConsultarMovimientosProductoSolicitud solicitud = new ConsultarMovimientosProductoSolicitud
        {
            IdProducto = 1,
        };

        var gestor = GestorDeInventario.GetInstance(this.configuration);
        RespuestaBase<List<ConsultarMovimientosProductoRespuesta>> respuesta = gestor.ConsultarMovimientosProducto(solicitud);

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ModificarProducto()
    {
        ModificarProductoSolicitud solicitud = new ModificarProductoSolicitud
        {
            IdProducto = 1,
            Nombre = "Nombre modificado prueba unitaria",
            GranoId = 2,
            ValorMinimo = 20
        };

        var gestor = GestorDeInventario.GetInstance(this.configuration);
        ModificarProductoRespuesta respuesta = gestor.ModificarProducto(solicitud);

        Assert.NotNull(respuesta);
        Assert.True(respuesta.RegistroModificadoExitosamente);
    }

    [Fact]
    public void AgregarMovimiento()
    {
        AgregarMovimientoSolicitud solicitud = new AgregarMovimientoSolicitud
        {
            IdProducto = 1,
            TipoMovimiento = 1,
            Cantidad = 1
        };

        var gestor = GestorDeInventario.GetInstance(this.configuration);
        AgregarMovimientoRespuesta respuesta = gestor.AgregarMovimiento(solicitud);

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo, 1));
    }

    [Fact]
    public void ObtenerAlertas()
    {
        var gestor = GestorDeInventario.GetInstance(this.configuration);
        RespuestaBase<List<ObtenerAlertasRespuesta>> respuesta = gestor.ObtenerAlertas();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ObtenerInfoProducto()
    {
        ObtenerInfoProductoSolicitud solicitud = new ObtenerInfoProductoSolicitud
        {
            IdProducto = 1,
        };
        var gestor = GestorDeInventario.GetInstance(this.configuration);
        RespuestaBase<ObtenerInfoProductoRespuesta> respuesta = gestor.ObtenerInfoProducto(solicitud);

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }
}

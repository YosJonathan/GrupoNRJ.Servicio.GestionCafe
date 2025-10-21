// <copyright file="Catalogos.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Servicio.GestionCafe.Singleton;
using Microsoft.Extensions.Configuration;

namespace GrupoNRJ.Pruebas.GestionCafe;

public class Catalogos
{
    private readonly IConfiguration configuration;

    public Catalogos()
    {
        // Construimos IConfiguration con esos valores
#pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
        this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(Validaciones.GenerarConfiguraciones())
            .Build();
#pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
    }

    [Fact]
    public void ObtenerGranos()
    {
        var gestor = GestorDeCatalogo.GetInstance(this.configuration);
        RespuestaBase<List<GranosRespuesta>> respuesta = gestor.ObtenerGranos();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ObtenerTipoProducto()
    {
        var gestor = GestorDeCatalogo.GetInstance(this.configuration);
        RespuestaBase<List<TipoProductoResponse>> respuesta = gestor.ObtenerTipoProducto();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void ObtenerCatalogoCombo()
    {
        var gestor = GestorDeCatalogo.GetInstance(this.configuration);
        RespuestaBase<ListadoCatalogoProductosRespuesta> respuesta = gestor.ObtenerCatalogoCombo();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }
}

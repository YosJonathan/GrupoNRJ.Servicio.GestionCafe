// <copyright file="Reportes.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Servicio.GestionCafe.Facade;
using Microsoft.Extensions.Configuration;

namespace GrupoNRJ.Pruebas.GestionCafe;

public class Reportes
{
    private readonly IConfiguration configuration;

    public Reportes()
    {
        // Construimos IConfiguration con esos valores
#pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
        this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(Validaciones.GenerarConfiguraciones())
            .Build();
#pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
    }

    [Fact]
    public void ObtenerReportes()
    {
        // Simulamos la configuración en memoria
        FacadeDeProduccion facadeDeProduccion = new(this.configuration);
        RespuestaBase<GeneracionReportesRespuesta> respuesta = facadeDeProduccion.GeneracionReportes();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }
}

using GrupoNRJ.Modelos.GestionCafe.Respuestas;
using GrupoNRJ.Modelos.GestionCafe;
using GrupoNRJ.Servicio.GestionCafe.Singleton;
using Microsoft.Extensions.Configuration;
using GrupoNRJ.Modelos.GestionCafe.Solicitudes;
using GrupoNRJ.Servicio.GestionCafe.Abstract_Factory;

namespace GrupoNRJ.Pruebas.GestionCafe;

public class Combos
{
    private readonly IConfiguration configuration;

    public Combos()
    {
        // Construimos IConfiguration con esos valores
#pragma warning disable CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
        this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(Validaciones.GenerarConfiguraciones())
            .Build();
#pragma warning restore CS8620 // El argumento no se puede usar para el parámetro debido a las diferencias en la nulabilidad de los tipos de referencia.
    }

    [Fact]
    public void AgregarCombo()
    {
        AgregarComboSolicitud solicitud = new();

        solicitud.Nombre = "Combo para nueva prueba unitaria";
        solicitud.Cafe = new ProductoSolicitud
        {
            Codigo = 1,
            Descripcion = "Nombre modificado prueba unitaria",
        };
        solicitud.Tasa = new ProductoSolicitud
        {
            Codigo = 1002,
            Descripcion = "Tasa chida",
        };
        solicitud.Filtro = new ProductoSolicitud
        {
            Codigo = 1004,
            Descripcion = "Filtro del bueno",
        };

        AgregarCombosRespuestas respuesta = new();
        IFabricaDeCombos fabrica = new ComboDesdeBDFactory(solicitud.Cafe, solicitud.Tasa, solicitud.Filtro);
        Combo combo = new Combo(fabrica);

        respuesta = combo.CreandoCombo(solicitud.Nombre, this.configuration);

        Assert.NotNull(respuesta);
        Assert.True(respuesta.ComboAgregado);
    }

    [Fact]
    public void ObtenerListadoCombos()
    {
        var gestor = GestorDeInventario.GetInstance(this.configuration);
        RespuestaBase<List<CombosResponse>> respuesta = gestor.ObtenerListadoCombos();

        Assert.NotNull(respuesta);
        Assert.True(Validaciones.ValidarCodigo(respuesta.Codigo));
    }

    [Fact]
    public void EliminarCombo()
    {
        EliminarCombo solicitud = new EliminarCombo
        {
            IdCombo = 2,
        };

        var gestor = GestorDeInventario.GetInstance(this.configuration);
        EliminarComboRespuesta respuesta = gestor.EliminarCombo(solicitud);

        Assert.NotNull(respuesta);
        Assert.True(respuesta.ComboEliminadoExitosamente);
    }
}

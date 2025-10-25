using GrupoNRJ.Modelos.GestionCafe.Respuestas;

namespace GrupoNRJ.Servicio.GestionCafe.Observer
{
    public class NotificadorConsola : IObserver
    {
        /// <summary>
        /// Metodo actualizar los productos.
        /// </summary>
        /// <param name="productosBajos">Productos bajos.</param>
        public void Update(List<ObtenerAlertasRespuesta> productosBajos)
        {
            foreach (var p in productosBajos)
            {
                Console.WriteLine( $"<{p.NombreProducto}> Min: {p.CantidadMinima} - Existencias: {p.Existencias}");
            }
        }
    }
}

using GrupoNRJ.Modelos.GestionCafe.Respuestas;

namespace GrupoNRJ.Servicio.GestionCafe.Observer
{
    /// <summary>
    /// Interfaz de observador.
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Metodo actualizar los productos.
        /// </summary>
        /// <param name="productosBajos">Productos bajos.</param>
        void Update(List<ObtenerAlertasRespuesta> productosBajos);
    }
}

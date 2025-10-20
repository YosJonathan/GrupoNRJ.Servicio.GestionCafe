namespace GrupoNRJ.Servicio.GestionCafe.Abstract_Factory
{
    /// <summary>
    /// Interfaz de cafe.
    /// </summary>
    public interface ICafe
    {
        /// <summary>
        /// Obtener la descripción de cafe.
        /// </summary>
        /// <returns>Descripcion de cafe.</returns>
        string Descripcion();

        /// <summary>
        /// Obtener el código de cafe.
        /// </summary>
        /// <returns>Código de cafe.</returns>
        int Codigo();
    }
}

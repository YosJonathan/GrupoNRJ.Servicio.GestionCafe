namespace GrupoNRJ.Servicio.GestionCafe
{
    public class GestorDeInventario
    {
        private static GestorDeInventario? _instancia;
        private static readonly object _lock = new();

        private readonly Dictionary<int, int> _inventario;

        // 🔒 Constructor privado
        private GestorDeInventario()
        {
            _inventario = new Dictionary<int, int>();
        }

        // 🔑 Acceso Singleton
        public static GestorDeInventario Instancia
        {
            get
            {
                lock (_lock)
                {
                    _instancia ??= new GestorDeInventario();
                    return _instancia;
                }
            }
        }

        // 📌 Métodos principales
        public void AgregarProducto(int idProducto, int cantidad)
        {
            if (_inventario.ContainsKey(idProducto))
                _inventario[idProducto] += cantidad;
            else
                _inventario[idProducto] = cantidad;
        }

        public void RetirarProducto(int idProducto, int cantidad)
        {
            if (_inventario.ContainsKey(idProducto) && _inventario[idProducto] >= cantidad)
                _inventario[idProducto] -= cantidad;
        }

        public int ConsultarStock(int idProducto)
        {
            return _inventario.ContainsKey(idProducto) ? _inventario[idProducto] : 0;
        }

        public Dictionary<int, int> ObtenerInventario()
        {
            return _inventario;
        }
    }

}

using Microsoft.Data.SqlClient;
using System.Data;

namespace GrupoNRJ.Servicio.GestionCafe
{
    public class EjecutarSP
    {
        private readonly string _connectionString;

        public EjecutarSP(IConfiguration configuration)
        {
            // Lee la cadena de conexión desde appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado y devuelve un DataTable con los resultados.
        /// </summary>
        public DataTable ExecuteStoredProcedure(string storedProcedure, Dictionary<string, object>? parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = new SqlCommand(storedProcedure, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                var resultTable = new DataTable();

                connection.Open();
                adapter.Fill(resultTable);

                return resultTable;
            }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que no devuelve datos (INSERT, UPDATE, DELETE).
        /// </summary>
        public bool ExecuteNonQuery(string storedProcedure, Dictionary<string, object>? parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true; // ✅ Se ejecutó correctamente
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar el SP '{storedProcedure}': {ex.Message}");
                return false; // ❌ Falló la ejecución
            }
        }
    }
}

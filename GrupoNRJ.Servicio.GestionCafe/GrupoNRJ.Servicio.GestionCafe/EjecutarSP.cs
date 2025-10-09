// <copyright file="EjecutarSP.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe
{
    using System.Data;
    using Microsoft.Data.SqlClient;

    public class EjecutarSP
    {
        private readonly string _connectionString;

        public EjecutarSP(IConfiguration configuration, bool bitacora = false)
        {
            // Lee la cadena de conexión desde appsettings.json
            if (bitacora)
            {
                _connectionString = configuration.GetConnectionString("Configuracion")
                    ?? throw new InvalidOperationException("Connection string not found.");
            }
            else
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string not found.");
            }
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
    }
}

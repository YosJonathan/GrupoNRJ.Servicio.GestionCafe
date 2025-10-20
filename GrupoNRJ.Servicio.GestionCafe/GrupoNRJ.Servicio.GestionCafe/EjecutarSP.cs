// <copyright file="EjecutarSP.cs" company="GrupoAnalisis">
// Copyright (c) GrupoAnalisis. All rights reserved.
// </copyright>

namespace GrupoNRJ.Servicio.GestionCafe
{
    using System.Data;
    using Microsoft.Data.SqlClient;

    public class EjecutarSP
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="EjecutarSP"/> class.
        /// </summary>
        /// <param name="configuration">Objeto de configuración.</param>
        /// <param name="bitacora">Bitacora.</param>
        /// <exception cref="InvalidOperationException">Excepción de conexión no encontrada.</exception>
        public EjecutarSP(IConfiguration configuration, bool bitacora = false)
        {
            // Lee la cadena de conexión desde appsettings.json
            if (bitacora)
            {
                this.connectionString = configuration.GetConnectionString("Configuracion")
                    ?? throw new InvalidOperationException("Connection string not found.");
            }
            else
            {
                this.connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string not found.");
            }
        }

        /// <summary>
        /// Ejecuta los procedimientos almacenados.
        /// </summary>
        /// <param name="storedProcedure">Nombre de SP.</param>
        /// <param name="parameters">Parametros.</param>
        /// <returns>Respuesta de procedimiento.</returns>
        public DataTable ExecuteStoredProcedure(string storedProcedure, Dictionary<string, object>? parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
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
        /// Ejecuta un procedimiento almacenado sin respuesta.
        /// </summary>
        /// <param name="storedProcedure">Nombre de SP.</param>
        /// <param name="parameters">Parametros.</param>
        /// <returns>Confirmación de SP ejecutado correctamente.</returns>
        public bool ExecuteNonQuery(string storedProcedure, Dictionary<string, object>? parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
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

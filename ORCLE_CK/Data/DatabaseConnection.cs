using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
namespace ORCLE_CK.Data
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString;
        private static readonly int commandTimeout;

        static DatabaseConnection()
        {
            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["OracleConnection"];
                if (connStr == null)
                {
                    throw new InvalidOperationException("Connection string 'OracleConnection' not found in configuration");
                }

                connectionString = connStr.ConnectionString;
                commandTimeout = int.Parse(ConfigurationManager.AppSettings["DatabaseTimeout"] ?? "30");

                Logger.LogInfo("Database connection configuration loaded successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to initialize database connection: {ex.Message}", ex);
                throw;
            }
        }

        public static OracleConnection GetConnection()
        {
            try
            {
                var connection = new OracleConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Database connection error: {ex.Message}", ex);
                throw new Exception($"Không thể kết nối database: {ex.Message}");
            }
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    // Test với một query đơn giản
                    using (var command = new OracleCommand("SELECT 1 FROM DUAL", connection))
                    {
                        command.CommandTimeout = commandTimeout;
                        var result = command.ExecuteScalar();
                        return result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Database connection test failed: {ex.Message}", ex);
                return false;
            }
        }

        public static OracleCommand CreateCommand(string sql, OracleConnection connection)
        {
            var command = new OracleCommand(sql, connection);
            command.CommandTimeout = commandTimeout;
            return command;
        }

        public static void SetConnectionString(string server, string port, string serviceName, string username, string password)
        {
            try
            {
                var newConnectionString = $"Data Source={server}:{port}/{serviceName};User Id={username};Password={password};Connection Timeout={commandTimeout};";

                // Update configuration
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["OracleConnection"].ConnectionString = newConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");

                Logger.LogInfo("Connection string updated successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to update connection string: {ex.Message}", ex);
                throw;
            }
        }

        public static string GetConnectionInfo()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return $"Server: {connection.DataSource}, Database: {connection.DatabaseName}";
                }
            }
            catch (Exception ex)
            {
                return $"Connection failed: {ex.Message}";
            }
        }
    }
}

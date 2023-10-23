using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HRMSystemDataAccess
{
    public class Connection : IDisposable
    {
        private readonly SqlConnection? conn;
        private bool _disposed = false;  // to track whether Dispose has been called
        private readonly IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        public Connection()
        {
            IConfigurationRoot configuration = builder.Build();
            conn = configuration["dbConn"] != null ? new SqlConnection(configuration["dbConn"]) : null;
            GetOpenConnection();
        }

        public SqlConnection GetOpenConnection()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return conn;
            }
            else
            {
                throw new InvalidOperationException("The database connection string is missing or invalid.");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Closed)
                        {
                            conn.Close();
                        }
                        conn.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        // Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
        }
    }
}

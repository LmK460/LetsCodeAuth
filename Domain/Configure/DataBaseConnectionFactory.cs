using MinimalLetsApiAuth.Domain.Interfaces;
using Npgsql;

namespace MinimalLetsApiAuth.Domain.Configure
{
    public class DataBaseConnectionFactory : IDatabaseConnectionFactory
    {
        private string ConnectionString { get; }

        public DataBaseConnectionFactory(string connectionString) =>
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

        public async Task<NpgsqlConnection> GetConnectionFactoryAsync()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}

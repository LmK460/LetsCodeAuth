using Npgsql;

namespace MinimalLetsApiAuth.Domain.Interfaces
{
    public interface IDatabaseConnectionFactory
    {

        Task<NpgsqlConnection> GetConnectionFactoryAsync();
    }
}

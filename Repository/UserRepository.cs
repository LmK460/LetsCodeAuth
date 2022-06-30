using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.DTO;
using MinimalLetsApiAuth.Models;
using Npgsql;
using System.Data;

namespace MinimalLetsApiAuth.Repository
{
    public class UserRepository : IUserRepository
    {
        public IDatabaseConnectionFactory DatabaseConnectionFactory { get; }

        public UserRepository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            DatabaseConnectionFactory = databaseConnectionFactory;
        }

        public async Task<Role.RoleType> GetRole(UserLoginDTO userLoginDTO)
        {
            using (var conn = await DatabaseConnectionFactory.GetConnectionFactoryAsync())
            {
                using (var cmd = new NpgsqlCommand("GET_USER_ROLE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", userLoginDTO.UserName);
                    cmd.Prepare();
                    var reader = cmd.ExecuteScalarAsync();
                    var result = (Role.RoleType)reader.Result;

                    return result;
                }

            }
        }


    }
}

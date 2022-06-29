using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.DTO;
using Npgsql;
using System.Data;

namespace MinimalLetsApiAuth.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public IDatabaseConnectionFactory DatabaseConnectionFactory { get; }

        public AuthRepository(IDatabaseConnectionFactory databaseConnectionFactory)
        {
            DatabaseConnectionFactory = databaseConnectionFactory;
        }

        public async Task<bool> Autenticate(UserLoginDTO userLoginDTO)
        {
            using(var conn = await DatabaseConnectionFactory.GetConnectionFactoryAsync())
            {
                using (var cmd = new NpgsqlCommand("VALID_USER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", userLoginDTO.UserName) ; 
                    cmd.Parameters.AddWithValue("pass", userLoginDTO.Password) ;
                    cmd.Prepare();
                    var reader = cmd.ExecuteScalarAsync();
                    Console.WriteLine(reader);
                    var result = Convert.ToBoolean(reader.Result);

                    if (result == true)
                    {
                        return true;
                    }
                    else
                        return false;
                }
               
            }
        }
    }
}

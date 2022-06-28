using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.Model;
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

        public async Task<bool> Autenticate(UserLoginDto userLoginDto)
        {
            using(var conn = await DatabaseConnectionFactory.GetConnectionFactoryAsync())
            {
                using (var cmd = new NpgsqlCommand("VALID_USER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("name", userLoginDto.UserName) ; 
                    cmd.Parameters.AddWithValue("pass", userLoginDto.Password) ;
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

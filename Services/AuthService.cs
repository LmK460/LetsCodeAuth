using MinimalLetsApiAuth.Domain.Configure;
using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.DTO;
using MinimalLetsApiAuth.Models;

namespace MinimalLetsApiAuth.Services
{
    public class AuthService
    {
        private IAuthRepository authRepository { get; }

        private IUserRepository userRepository { get; }

        private WebApplicationBuilder webApplicationBuilder { get; }

        public AuthService(IAuthRepository authRepository, IUserRepository userRepository, WebApplicationBuilder webApplicationBuilder)
        {
            this.authRepository = authRepository;
            this.userRepository = userRepository;
            this.webApplicationBuilder = webApplicationBuilder;
        }

        public async Task<LoginResponseDTO> Login(UserLoginDTO userLogingDTO)
        {
            try
            {
                var authResult = await authRepository.Autenticate(userLogingDTO);
                if (authResult ==true)
                {
                    //GENERATE TOKEN
                    
                    var role = await userRepository.GetRole(userLogingDTO);
                    var user = new User { Role = role, UserName = userLogingDTO.UserName };
                    var token = new TokenFactory(webApplicationBuilder).GenerateToken(user);
                    //CREATE RESPONSE
                    return new LoginResponseDTO { Autenticated = true, Message = "usuário autenticado com sucesso",AccessToken = token.AccessToken,
                                                 Created=token.Created,Expiration=token.Expiration,Role = user.Role.ToString()};
                }
                else
                {
                    return new LoginResponseDTO { Autenticated = false, Message = "Erro ao tentar autenticar" };
                }
            }
            catch (Exception)
            {
                return new LoginResponseDTO { Autenticated = false, Message = "Erro ao tentar autenticar" };
            }    

        }


    }
}

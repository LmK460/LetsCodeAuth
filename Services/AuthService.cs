using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.DTO;

namespace MinimalLetsApiAuth.Services
{
    public class AuthService
    {
        private IAuthRepository authRepository { get; }

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<LoginResponseDTO> Login(UserLoginDTO userLogingDTO)
        {
            try
            {
                var authResult = await authRepository.Autenticate(userLogingDTO);
                if (authResult ==true)
                {
                    //GENERATE TOKEN
                    //CREATE RESPONSE
                    return new LoginResponseDTO { Autenticated = true, Message = "usuário autenticado com sucesso" };
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

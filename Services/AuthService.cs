using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.Model;

namespace MinimalLetsApiAuth.Services
{
    public class AuthService
    {
        private IAuthRepository authRepository { get; }

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<bool> Login(UserLoginDto userLogingDto)
        {
            
            return await authRepository.Autenticate(userLogingDto); ;

        }


    }
}

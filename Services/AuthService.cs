using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.Model;

namespace MinimalLetsApiAuth.Services
{
    public class AuthService
    {
        public IAuthRepository authRepository { get; }

        public AuthService(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        public async Task<bool> Login(UserLoginDto userLogingDto)
        {
            var authResult = await authRepository.Autenticate(userLogingDto);
            Console.WriteLine(authResult);
            return authResult;

        }


    }
}

using MinimalLetsApiAuth.Model;

namespace MinimalLetsApiAuth.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> Autenticate(UserLoginDto userLoginDto);

    }
}

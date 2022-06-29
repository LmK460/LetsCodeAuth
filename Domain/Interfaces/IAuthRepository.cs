using MinimalLetsApiAuth.DTO;

namespace MinimalLetsApiAuth.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> Autenticate(UserLoginDTO userLoginDto);

    }
}

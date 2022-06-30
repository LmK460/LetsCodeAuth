using MinimalLetsApiAuth.Models;

namespace MinimalLetsApiAuth.Domain.Interfaces
{
    public interface ITokenFactory
    {
        Token GenerateToken(User user);
        Token GetToken(DateTime dataCriacao, DateTime dataExpiracao, string token);

    };
}

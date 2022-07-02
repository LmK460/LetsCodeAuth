using MinimalLetsApiAuth.Models;

namespace MinimalLetsApiAuth.Domain.Interfaces
{
    public interface ITokenFactory
    {
        Token GenerateToken(User user);

    };
}

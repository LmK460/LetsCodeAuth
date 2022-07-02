using MinimalLetsApiAuth.DTO;
using MinimalLetsApiAuth.Models;

namespace MinimalLetsApiAuth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Role.RoleType> GetRole(UserLoginDTO userLoginDTO);
    }
}

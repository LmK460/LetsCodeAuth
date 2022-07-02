using MinimalLetsApiAuth.Domain.Interfaces;
using MinimalLetsApiAuth.DTO;
using MinimalLetsApiAuth.Models;

namespace MinimalLetsApiAuth.Services
{
    public class UserService
    {
        private IUserRepository userRepository { get; }

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> ObtainUser(UserLoginDTO userLoginDTO)
        {
            var role = await userRepository.GetRole(userLoginDTO);
            User user = new User
            {
                UserName = userLoginDTO.UserName,
                Role = role
            };
            Console.WriteLine(user.Role.Value);
            return user;
        }

    }
}

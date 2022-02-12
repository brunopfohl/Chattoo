using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;

namespace Chattoo.Domain.Services
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserOrThrow(string userId)
        {
            return await _userRepository.GetByIdAsync(userId)
                   ?? throw new UserNotFoundException(userId);
        }
    }
}
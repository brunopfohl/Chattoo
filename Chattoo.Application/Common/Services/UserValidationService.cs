using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Common.Services
{
    public class UserValidationService
    {
        private readonly IUserRepository _userRepository;

        private User _user;
        
        public UserValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public bool Found(string channelId)
        {
            _user = _userRepository.GetById(channelId);

            return _user != null;
        }
    }
}
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.GraphQL.Services
{
    /// <summary>
    /// Třída reprezentující službu, která poskytuje Id aktuálně přihlášeného uživate.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private User _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserIdService _currentUserIdService;

        public CurrentUserService(IUserRepository userRepository, ICurrentUserIdService currentUserIdService)
        {
            _userRepository = userRepository;
            _currentUserIdService = currentUserIdService;
        }

        /// <summary>
        /// Vrací aktuálně přihlášeného uživatele.
        /// </summary>
        public User User => GetCurrentUser();

        private User GetCurrentUser()
        {
            if (_currentUser == null && _currentUserIdService.ClaimsPrincipal?.Identity?.IsAuthenticated == true)
            {
                _currentUser = _userRepository.GetById(_currentUserIdService.UserId);
            }

            return _currentUser;
        }
    }
}

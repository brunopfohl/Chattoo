using Chattoo.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Vrací Id aktuálně přihlášeného uživatele.
        /// </summary>
        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Vrací aktuálně přihlášeného uživatele.
        /// </summary>
        public User User
        {
            get
            {
                if (_currentUser is null || _currentUser.Id != this.UserId)
                {
                    _currentUser = _userRepository.GetByIdAsync(this.UserId).Result;
                }

                return _currentUser;
            }
        }
    }
}

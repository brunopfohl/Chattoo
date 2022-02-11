using System.Security.Claims;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace Chattoo.GraphQL.Services
{
    public class CurrentUserIdService : ICurrentUserIdService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserIdService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        /// <summary>
        /// Vrací Id aktuálně přihlášeného uživatele.
        /// </summary>
        public string UserId => ClaimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);
        
        public ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor.HttpContext?.User;
    }
}
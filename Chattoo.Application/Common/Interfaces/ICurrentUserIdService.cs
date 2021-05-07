using System.Security.Claims;

namespace Chattoo.Application.Common.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu poskytující Id aktuálně přihlášeného uživatele.
    /// </summary>
    public interface ICurrentUserIdService
    {
        /// <summary>
        /// Vrací Id aktuálně přihlášeného uživatele.
        /// </summary>
        public string UserId { get; }
        
        /// <summary>
        /// Vrací claimy aktuálně přihlášeného uživatele.
        /// </summary>
        public ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
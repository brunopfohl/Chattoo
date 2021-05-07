using Chattoo.Domain.Entities;

namespace Chattoo.Application.Common.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu poskytující aktuálně přihlášeného uživatele.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Vrací aktuálně přihlášeného uživatele.
        /// </summary>
        public User User { get; }
    }
}

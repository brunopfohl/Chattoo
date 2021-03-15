namespace Chattoo.Application.Common.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu poskytující Id aktuálně přihlášeného uživatele.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Vrací Id aktuálně přihlášeného uživatele.
        /// </summary>
        public string UserId { get; }
    }
}

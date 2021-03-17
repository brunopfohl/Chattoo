using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní pro repozitář komunikačních kanálů.
    /// </summary>
    public interface ICommunicationChannelRepository : IRepository<CommunicationChannel>
    {
        /// <summary>
        /// Vrací komunikační kanály, kterých je daný uživatel součástí.
        /// </summary>
        /// <param name="userId">Id uživatele, jehož komunikační kanály se mají načíst.</param>
        /// <returns>Kolekci <see cref="CommunicationChannel"/> komunikačních kanálu, kterých je uživatel součástí.</returns>
        public IQueryable<CommunicationChannel> GetByUserId(string userId);
    }
}
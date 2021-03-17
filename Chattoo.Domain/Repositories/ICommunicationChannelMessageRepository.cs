using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní pro repozitář zprávy z komunikačního kanálu.
    /// </summary>
    public interface ICommunicationChannelMessageRepository : IRepository<CommunicationChannelMessage>
    {
        /// <summary>
        /// Vrací všechny zprávy z komunikačního kanálu.
        /// </summary>
        /// <param name="channelId">Id komunikačního kanálu, jehož zprávy se mají načíst.</param>
        /// <returns>Kolekci <see cref="CommunicationChannelMessage"/> zpráv z daného komunikačního kanálu.</returns>
        public IQueryable<CommunicationChannelMessage> GetByChannelId(string channelId);
        
        /// <summary>
        /// Vrací všechny zprávy uživatele, které sdílel v komunikačním kanálu.
        /// </summary>
        /// <param name="channelId">Id komunikačního kanálu</param>
        /// <param name="userId">Id uživatele</param>
        /// <returns>Kolekci <see cref="CommunicationChannelMessage"/> zpráv uživatele, které sdílel v komunikačním kanálu.</returns>
        public IQueryable<CommunicationChannelMessage> GetForUserInChannel(string channelId, string userId);
    }
}
using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní pro repozitář role uživatele v komunikačním kanálu.
    /// </summary>
    public interface ICommunicationChannelRoleRepository : IRepository<CommunicationChannelRole>
    {
        /// <summary>
        /// Vrací uživatelské role dostupné v daném komunikačním kanálu.
        /// </summary>
        /// <param name="channelId">Id komunikačního kanálu, jehož dostupné role se mají načíst.</param>
        /// <returns>Kolekci <see cref="CommunicationChannelRole"/> uživatelských rolí z daného komunikačního kanálu.</returns>
        public IQueryable<CommunicationChannelRole> GetByChannelId(string channelId);
        
        /// <summary>
        /// Vrací uživatelské role daného uživatele v určitém komunikačním kanálu.
        /// </summary>
        /// <param name="userId">Id uživatele, jehož role se mají načíst.</param>
        /// <param name="channelId">Id komunikačního kanálu, ze kterého se mají role načíst.</param>
        /// <returns>Kolekci <see cref="CommunicationChannelRole"/> uživatelských rolí daného uživatele v určitém komunikačním kanálu.</returns>
        public IQueryable<CommunicationChannelRole> GetForUserInChannel(string userId, string channelId);
    }
}
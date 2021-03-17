using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře příloh u zpráv z komunikačních kanálů.
    /// </summary>
    public interface ICommunicationChannelMessageAttachmentRepository : IRepository<CommunicationChannelMessageAttachment>
    {
        /// <summary>
        /// Vrací přílohy připnuté k dané zprávě.
        /// </summary>
        /// <param name="messageId">Id zprávy, jejíž příspěvky se mají načíst.</param>
        /// <returns>Kolekci <see cref="CommunicationChannelMessageAttachment"/> příloh u zprávy.</returns>
        public IQueryable<CommunicationChannelMessageAttachment> GetByMessageId(string messageId);
        
        /// <summary>
        /// Vrací přílohy, které byly připnuty ke zprávám z daného komunikačního kanálu.
        /// </summary>
        /// <param name="channelId">Id komunikačního kanálu, jehož přílohy se mají načíst.</param>
        /// <returns>Kolekci <see cref="CommunicationChannelMessageAttachment"/> příloh u zpráv daného kanálu.</returns>
        public IQueryable<CommunicationChannelMessageAttachment> GetByChannelId(string channelId);
    }
}
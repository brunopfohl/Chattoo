using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    public interface IChannelMessageRepository : IReadOnlyRepository<CommunicationChannelMessage>
    {
        IQueryable<CommunicationChannelMessage> GetForChannel(string channelId);
    }
}
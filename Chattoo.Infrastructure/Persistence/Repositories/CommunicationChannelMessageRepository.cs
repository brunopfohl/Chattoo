using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repozitář zpráv z komunikačních kanálů.
    /// </summary>
    public class CommunicationChannelMessageRepository : Repository<CommunicationChannelMessage>, ICommunicationChannelMessageRepository
    {
        public CommunicationChannelMessageRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<CommunicationChannelMessage> GetByChannelId(string channelId)
        {
            var result = GetAll()
                .Where(m => m.ChannelId == channelId);

            return result;
        }

        public IQueryable<CommunicationChannelMessage> GetForUserInChannel(string channelId, string userId)
        {
            var result = GetByChannelId(channelId)
                .Where(m => m.UserId == userId);

            return result;
        }
    }
}
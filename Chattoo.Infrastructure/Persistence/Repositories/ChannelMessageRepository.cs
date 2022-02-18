using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    public class ChannelMessageRepository : ReadOnlyRepository<CommunicationChannelMessage>, IChannelMessageRepository
    {
        public ChannelMessageRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<CommunicationChannelMessage> GetForChannel(string channelId)
        {
            return GetAll().Where(m => m.ChannelId == channelId);
        }
    }
}
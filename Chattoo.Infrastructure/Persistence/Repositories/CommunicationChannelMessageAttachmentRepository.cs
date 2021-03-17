using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repozitář příloh zpráv z komunikačních kanálů.
    /// </summary>
    public class CommunicationChannelMessageAttachmentRepository : Repository<CommunicationChannelMessageAttachment>, ICommunicationChannelMessageAttachmentRepository
    {
        public CommunicationChannelMessageAttachmentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<CommunicationChannelMessageAttachment> GetByMessageId(string messageId)
        {
            var result = GetAll()
                .Where(a => a.MessageId == messageId);

            return result;
        }

        public IQueryable<CommunicationChannelMessageAttachment> GetByChannelId(string channelId)
        {
            var result = GetAll()
                .Where(a => a.Message.ChannelId == channelId);

            return result;
        }
    }
}
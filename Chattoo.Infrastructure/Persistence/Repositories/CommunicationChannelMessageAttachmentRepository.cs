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
    }
}
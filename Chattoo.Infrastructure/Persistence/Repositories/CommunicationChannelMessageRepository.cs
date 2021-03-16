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
    }
}
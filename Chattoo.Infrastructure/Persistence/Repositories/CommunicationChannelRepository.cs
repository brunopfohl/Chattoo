using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repozitář komunikačních kanálů.
    /// </summary>
    public class CommunicationChannelRepository : Repository<CommunicationChannel>, ICommunicationChannelRepository
    {
        public CommunicationChannelRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
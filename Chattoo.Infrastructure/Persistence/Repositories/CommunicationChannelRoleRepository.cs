using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repozitář rolí uživatelů napříč komunikačními kanály.
    /// </summary>
    public class CommunicationChannelRoleRepository : Repository<CommunicationChannelRole>, ICommunicationChannelRoleRepository
    {
        public CommunicationChannelRoleRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
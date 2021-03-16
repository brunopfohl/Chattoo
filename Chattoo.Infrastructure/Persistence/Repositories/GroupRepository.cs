using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře skupin uživatelů.
    /// </summary>
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
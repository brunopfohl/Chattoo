using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře rolí uživatelů napříč skupinami.
    /// </summary>
    public class GroupRoleRepository : Repository<GroupRole>, IGroupRoleRepository
    {
        public GroupRoleRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
using System.Linq;
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

        public IQueryable<GroupRole> GetByGroupId(string groupId)
        {
            var result = GetAll()
                .Where(gr => gr.GroupId == groupId);

            return result;
        }

        public IQueryable<GroupRole> GetForUserInGroup(string userId, string groupId)
        {
            // TODO: Nesmysl. Musím jít nějak přes usera.
            var result = GetByGroupId(groupId)
                .Where(gr => gr.Users.Any(u => u.Id == userId));

            return result;
        }
    }
}
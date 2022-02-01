using System.Linq;
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

        public IQueryable<Group> GetByUserId(string userId)
        {
            var result = GetAll()
                .Where(g => g.Participants.Any(u => u.UserId == userId));

            return result;
        }
    }
}
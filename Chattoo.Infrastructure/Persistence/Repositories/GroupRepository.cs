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
            // TODO: Měl bych jít rovnou přes usera nebo to chce entitu reprezentující vazební tabulku.
            var result = GetAll()
                .Where(g => g.Users.Any(u => u.Id == userId));

            return result;
        }
    }
}
using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře aliasů (přezdívek) uživatelů.
    /// </summary>
    public class UserAliasRepository : Repository<UserAlias>, IUserAliasRepository
    {
        public UserAliasRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<UserAlias> GetByUserId(string userId)
        {
            var result = GetAll()
                .Where(a => a.UserId == userId);

            return result;
        }
    }
}
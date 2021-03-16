using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře uživatelů aplikace.
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
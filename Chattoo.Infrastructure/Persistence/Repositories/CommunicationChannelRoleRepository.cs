using System.Linq;
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

        public IQueryable<CommunicationChannelRole> GetByChannelId(string channelId)
        {
            var result = GetAll()
                .Where(r => r.ChannelId == channelId);

            return result;
        }

        public IQueryable<CommunicationChannelRole> GetForUserInChannel(string userId, string channelId)
        {
            // TODO: Zase hovadina. Potřebuju buď entitu reprezentující vazební tabulku mezi role a user nebo musim přes usera.
            
            var result = GetByChannelId(channelId)
                .Where(r => r.Users.Any(u => u.Id == userId));

            return result;
        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Castle.Core.Internal;
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

        public IQueryable<User> GetByChannelId(string channelId)
        {
            // TODO: Nesmysl, musím jít přes channel.
            var result = GetAll()
                .Where(u => u.Channels.Any(c => c.Id == channelId));

            return result;
        }

        public IQueryable<User> GetByGroupId(string groupId)
        {
            // TODO: Nesmysl, musím jít přes group.
            var result = GetAll()
                .Where(u => u.Groups.Any(g => g.Id == groupId));

            return result;
        }
        
        public IQueryable<User> GetBySearchTerm(string searchTerm, string excludeUsersFromCommunicationChannelWithId)
        {
            // TODO: Nesmysl, musím jít přes group.
            var result = GetAll()
                .Where(u =>
                    searchTerm.IsNullOrEmpty() || u.UserName.ToLower().Contains(searchTerm.ToLower())
                );

            // Pokud bylo specifikováno Id komunikačního kanálu, jehož uživatelé se mají vynechat, omezím query.
            if (!excludeUsersFromCommunicationChannelWithId.IsNullOrEmpty())
            {
                result = result.Where(u => u.Channels.All(c => c.Id != excludeUsersFromCommunicationChannelWithId));
            }

            return result;
        }
    }
}
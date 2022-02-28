using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Extensions;
using Chattoo.Domain.Repositories;
using CollectionExtensions = Castle.Core.Internal.CollectionExtensions;

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
            var result = GetAll()
                .Where(u => u.Channels.Any(ch => ch.ChannelId == channelId));

            return result;
        }

        public IQueryable<User> GetByGroupId(string groupId)
        {
            var result = GetAll()
                .Where(u => u.Groups.Any(g => g.GroupId == groupId));

            return result;
        }
        
        public IQueryable<User> GetByCalendarEventId(string calendarEventId)
        {
            var result = GetAll()
                .Where(u => u.JoinedEvents.Any(e => e.EventId == calendarEventId));

            return result;
        }
        
        public IQueryable<User> GetBySearchTerm(string searchTerm, List<string> excludedUserIds,
            string channelId = null, string groupId = null)
        {
            var result = GetAll()
                .Where(u =>
                    CollectionExtensions.IsNullOrEmpty(searchTerm) || u.UserName.ToLower().Contains(searchTerm.ToLower())
                );

            if (channelId.IsNotNullOrEmpty())
            {
                result = result.Where(u => u.Channels.Any(utc => utc.ChannelId == channelId));
            }
            
            if (groupId.IsNotNullOrEmpty())
            {
                result = result.Where(u => u.Groups.Any(utc => utc.GroupId == groupId));
            }

            if (excludedUserIds?.Count > 0)
            {
                result = result.Where(u => !excludedUserIds.Contains(u.Id));
            }

            return result;
        }
    }
}
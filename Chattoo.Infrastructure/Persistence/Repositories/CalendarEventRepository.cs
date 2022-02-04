using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repozitář kalendářních událostí.
    /// </summary>
    public class CalendarEventRepository : Repository<CalendarEvent>, ICalendarEventRepository
    {
        public CalendarEventRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<CalendarEvent> GetByCommunicationChannelId(string communicationChannelId)
        {
            var result = GetAll()
                .Where(ce => ce.CommunicationChannelId == communicationChannelId);

            return result;
        }

        public IQueryable<CalendarEvent> GetByGroupId(string groupId)
        {
            var result = GetAll()
                .Where(ce => ce.GroupId == groupId);

            return result;
        }
    }
}
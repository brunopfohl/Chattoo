using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repozitář kalendářních událostí z komunikačního kanálu.
    /// </summary>
    public class CommunicationChannelCalendarEventRepository : Repository<CommunicationChannelCalendarEvent>, ICommunicationChannelCalendarEventRepository
    {
        public CommunicationChannelCalendarEventRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<CommunicationChannelCalendarEvent> GetByCommunicationChannelId(string communicationChannelId)
        {
            var result = GetAll()
                .Where(ce => ce.CommunicationChannelId == communicationChannelId);

            return result;
        }
    }
}
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    public class CalendarEventTypeRepository : ReadOnlyRepository<CalendarEventType>, ICalendarEventTypeRepository
    {
        public CalendarEventTypeRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
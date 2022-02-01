using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    public class CalendarEventWishRepository : Repository<CalendarEventWish>, ICalendarEventWishRepository
    {
        public CalendarEventWishRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
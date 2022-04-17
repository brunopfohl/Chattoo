using System.Linq;
using AutoMapper;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Extensions;
using Chattoo.Domain.Repositories;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    public class CalendarEventWishRepository : Repository<CalendarEventWish>, ICalendarEventWishRepository
    {
        public CalendarEventWishRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IQueryable<CalendarEventWish> GetActiveByUserId(string userId)
        {
            return GetActive().Where(w => w.AuthorId == userId);
        }

        public IQueryable<CalendarEventWish> GetAddeptsFor(CalendarEventWish wish)
        {
            return GetActive().Where(w =>
                w.Type == wish.Type &&
                w.CommunicationChannelId == wish.CommunicationChannelId &&
                w.CalendarEventId.IsNullOrEmpty() &&
                w.AuthorId != wish.AuthorId
            );
        }
        
        private IQueryable<CalendarEventWish> GetActive()
        {
            return GetAll().Where(w => !w.DeletedAt.HasValue);
        }
    }
}
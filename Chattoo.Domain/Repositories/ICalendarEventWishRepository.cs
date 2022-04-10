using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Repozitář pro práci se záznamy o přání k vytvoření kalendářní události.
    /// </summary>
    public interface ICalendarEventWishRepository : IRepository<CalendarEventWish>
    {
        public IQueryable<CalendarEventWish> GetActiveByUserId(string userId);
        
        public IQueryable<CalendarEventWish> GetAddeptsFor(CalendarEventWish wish);
    }
}
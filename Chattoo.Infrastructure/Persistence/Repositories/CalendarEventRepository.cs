using System;
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
            var result = GetNonDeleted()
                .Where(ce => ce.CommunicationChannelId == communicationChannelId);

            return result;
        }

        public IQueryable<CalendarEvent> GetAddeptsForWish(CalendarEventWish wish)
        {
            // Nadcházející události
            var result = GetUpcoming().Where(e =>
                e.CalendarEventType == wish.Type && // se stejným typem
                e.CommunicationChannelId == wish.CommunicationChannelId && // ve stejném komunikačním kanálu
                (
                    !e.MaximalParticipantsCount.HasValue ||
                    (
                        e.MaximalParticipantsCount.Value >= wish.MinimalParticipantsCount && // může obsahovat dostatečný počet účastníků
                        e.Participants.Count() < e.MaximalParticipantsCount.Value // s volnou kapacitou
                    )
                ) &&
                e.Participants.Count() >= wish.MinimalParticipantsCount - 1 && // pokrývá minimální počet účastníků
                e.Participants.All(p => p.UserId != wish.AuthorId) // neúčastní se
            );

            return result;
        }

        public IQueryable<CalendarEvent> GetUpcoming()
        {
            var result = GetNonDeleted()
                .Where(e => e.StartsAt >= DateTime.Now);

            return result;
        }

        public IQueryable<CalendarEvent> GetNonDeleted()
        {
            var result = GetAll().Where(e => !e.DeletedAt.HasValue);
            return result;
        }
    }
}
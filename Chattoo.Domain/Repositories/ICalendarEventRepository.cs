using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní pro repozitář kalendářních událostí z komunikačních kanálů.
    /// </summary>
    public interface ICalendarEventRepository : IRepository<CalendarEvent>
    {
        /// <summary>
        /// Vrací kalendářní události u určitého komunikačního kanálu.
        /// </summary>
        /// <param name="communicationChannelId">Id komunikačního kanálu, jehož kalendářní události se mají načíst..</param>
        /// <returns>Kolekci <see cref="CalendarEvent"/> kalendářních událostí z určitého komunikačního kanálu.</returns>
        public IQueryable<CalendarEvent> GetByCommunicationChannelId(string communicationChannelId);
    }
}
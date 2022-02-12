using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;

namespace Chattoo.Domain.Services
{
    public class CalendarEventTypeManager
    {
        private readonly ICalendarEventTypeRepository _calendarEventTypeRepository;

        public CalendarEventTypeManager(ICalendarEventTypeRepository calendarEventTypeRepository)
        {
            _calendarEventTypeRepository = calendarEventTypeRepository;
        }

        public async Task<CalendarEventType> GetOrThrow(string typeId)
        {
            return await _calendarEventTypeRepository.GetByIdAsync(typeId)
                ?? throw new CalendarEventTypeNotFoundException(typeId);
        }
    }
}
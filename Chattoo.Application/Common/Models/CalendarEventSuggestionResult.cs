using Chattoo.Application.CalendarEvents.DTOs;

namespace Chattoo.Application.Common.Models
{
    public class CalendarEventSuggestionResult
    {
        public CalendarEventSuggestionResult(CalendarEventDto calendarEvent, bool eventWasCreated)
        {
            CalendarEvent = calendarEvent;
            EventWasCreated = eventWasCreated;
        }

        public CalendarEventDto CalendarEvent { get; private set; }

        public bool EventWasCreated { get; private set; }
    }
}
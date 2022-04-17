using System;
using System.Collections.Concurrent;
using Chattoo.Application.CalendarEvents.DTOs;

namespace Chattoo.GraphQL.Subscription.CalendarEvent
{
    public interface ICalendarEventSubscriptionProvider
    {
        IObservable<CalendarEventJoinedByUserEvent> CalendarEvents();
        CalendarEventJoinedByUserEvent OnUserJoinedCalendarEvent(CalendarEventDto calendarEvent, string userId);
        
        ConcurrentStack<CalendarEventJoinedByUserEvent> AllEvents { get; } 
    }
}
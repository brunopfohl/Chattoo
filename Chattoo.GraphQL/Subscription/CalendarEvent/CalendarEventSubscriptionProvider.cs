using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Chattoo.Application.CalendarEvents.DTOs;

namespace Chattoo.GraphQL.Subscription.CalendarEvent
{
    public class CalendarEventSubscriptionProvider : ICalendarEventSubscriptionProvider
    {
        private readonly ISubject<CalendarEventJoinedByUserEvent> _eventStream = new ReplaySubject<CalendarEventJoinedByUserEvent>(1);

        public CalendarEventSubscriptionProvider()
        {
            AllEvents = new ConcurrentStack<CalendarEventJoinedByUserEvent>();
        }

        public IObservable<CalendarEventJoinedByUserEvent> CalendarEvents()
        {
            return _eventStream.AsObservable();
        }

        public CalendarEventJoinedByUserEvent OnUserJoinedCalendarEvent(CalendarEventDto calendarEvent, string userId)
        {
            var newEvent = new CalendarEventJoinedByUserEvent(calendarEvent, userId);
            _eventStream.OnNext(newEvent);
            return newEvent;
        }

        public ConcurrentStack<CalendarEventJoinedByUserEvent> AllEvents { get; }
    }

    public class CalendarEventJoinedByUserEvent
    {
        public CalendarEventJoinedByUserEvent(CalendarEventDto calendarEvent, string userId)
        {
            CalendarEvent = calendarEvent;
            UserId = userId;
        }
        
        public CalendarEventDto CalendarEvent { get; }

        public string UserId { get; }
    }
}
using System;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Extensions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Domain.Services
{
    public class CalendarEventManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly GroupManager _groupManager;
        private readonly ChannelManager _channelManager;

        public CalendarEventManager(ICurrentUserService currentUserService, ICalendarEventRepository calendarEventRepository,
            GroupManager groupManager, ChannelManager channelManager)
        {
            _currentUserService = currentUserService;
            _calendarEventRepository = calendarEventRepository;
            _groupManager = groupManager;
            _channelManager = channelManager;
        }

        public async Task<CalendarEvent> GetEventOrThrow(string eventId)
        {
            var calendarEvent = await _calendarEventRepository.GetByIdAsync(eventId)
                ?? throw new CalendarEventNotFoundException(eventId);

            // Pokud uživatel není mezi účastníky kalendářní události.
            if (!calendarEvent.HasParticipant(_currentUserService.User.Id))
            {
                // Zkontroluji, jestli je ze stejného komunikačního kanálu.
                var userInSameChannel = 
                        calendarEvent.CommunicationChannelId.IsNullOrEmpty() ||
                        _currentUserService.CanViewChannel(calendarEvent.CommunicationChannelId);
                
                // Zkontroluji, jestli je ze stejné skupiny.
                var userInSameGroup = 
                        calendarEvent.GroupId.IsNullOrEmpty() ||
                        _currentUserService.CanViewGroup(calendarEvent.GroupId);
                
                // Pokud uživatel není mezi účastníky, v komunikačním kanálu nebo skupině, nemá na tuto událost nárok.
                if (!userInSameChannel && !userInSameGroup)
                {
                    throw new ForbiddenAccessException();
                }
            }

            return calendarEvent;
        }

        public async Task<CalendarEvent> CreateEvent(string channelId, string groupId, CalendarEventType type, string name, string description,
            int? maximalParticipants, DateTime startsAt, DateTime? endsAt)
        {
            CalendarEvent calendarEvent;

            if (channelId.IsNotNullOrEmpty())
            {
                var channel = await _channelManager.GetChannelOrThrow(channelId);

                calendarEvent = CalendarEvent.Create(_currentUserService.User, channel, type, name, description);
            }
            else
            {
                var group = await _groupManager.GetGroupOrThrow(groupId);

                calendarEvent = CalendarEvent.Create(_currentUserService.User, group, type, name, description);
            }
            
            calendarEvent.SetEndsAt(endsAt);
            calendarEvent.SetStartsAt(startsAt);
            calendarEvent.SetMaximalParticipantsCount(maximalParticipants);
            
            calendarEvent.AddParticipant(_currentUserService.User.Id);

            return calendarEvent;
        }

        public void AddParticipant(CalendarEvent calendarEvent, User user)
        {
            if (!_currentUserService.CanViewEvent(calendarEvent))
            {
                throw new ForbiddenAccessException();
            }
            
            calendarEvent.AddParticipant(user.Id);
        }
        
        public void RemoveParticipant(CalendarEvent calendarEvent, string userId)
        {
            if (!_currentUserService.CanViewEvent(calendarEvent))
            {
                throw new ForbiddenAccessException();
            }
            
            calendarEvent.RemoveParticipant(userId);
        }

        public async Task<CalendarEvent> UpdateEvent(CalendarEvent calendarEvent, string name, string description,
            int? maximalParticipants, DateTime startsAt, DateTime? endsAt)
        {
            if (!_currentUserService.CanEditEvent(calendarEvent))
            {
                throw new ForbiddenAccessException();
            }
            
            calendarEvent.SetName(name);
            calendarEvent.SetDescription(description);
            calendarEvent.SetEndsAt(endsAt);
            calendarEvent.SetStartsAt(startsAt);
            calendarEvent.SetMaximalParticipantsCount(maximalParticipants);

            return calendarEvent;
        }

        public async Task DeleteEvent(string eventId)
        {
            var calendarEvent = await GetEventOrThrow(eventId);

            if (!_currentUserService.CanEditEvent(calendarEvent))
            {
                throw new ForbiddenAccessException();
            }
            
            _calendarEventRepository.Remove(calendarEvent);
        }
    }
}
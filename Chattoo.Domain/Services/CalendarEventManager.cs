using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
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

        public CalendarEventManager(ICurrentUserService currentUserService, ICalendarEventRepository calendarEventRepository, GroupManager groupManager, ChannelManager channelManager)
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
    }
}
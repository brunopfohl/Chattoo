using System.Linq;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.GraphQL.Services
{
    /// <summary>
    /// Třída reprezentující službu, která poskytuje Id aktuálně přihlášeného uživate.
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private User _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserIdService _currentUserIdService;

        public CurrentUserService(IUserRepository userRepository, ICurrentUserIdService currentUserIdService)
        {
            _userRepository = userRepository;
            _currentUserIdService = currentUserIdService;
        }

        /// <summary>
        /// Vrací aktuálně přihlášeného uživatele.
        /// </summary>
        public User User => GetCurrentUser();

        private User GetCurrentUser()
        {
            if (_currentUser == null && _currentUserIdService.ClaimsPrincipal?.Identity?.IsAuthenticated == true)
            {
                _currentUser = _userRepository.GetById(_currentUserIdService.UserId);
            }

            return _currentUser;
        }
        
        public bool CanViewChannel(CommunicationChannel channel)
        {
            return User.Channels.Any(ch => ch.ChannelId == channel.Id);
        }

        public bool CanEditChannel(CommunicationChannel channel)
        {
            return CanViewChannel(channel);
        }
        
        public bool CanViewGroup(Group group)
        {
            return User.Groups.Any(ch => ch.GroupId == group.Id);
        }

        public bool CanEditGroup(Group group)
        {
            return CanViewGroup(group);
        }
        
        public bool CanViewEvent(CalendarEvent calendarEvent)
        {
            return User.JoinedEvents.Any(ch => ch.EventId == calendarEvent.Id);
        }
        
        public bool CanEditEvent(CalendarEvent calendarEvent)
        {
            return calendarEvent.AuthorId == User.Id;
        }

        public bool CanViewWish(CalendarEventWish wish)
        {
            return true;
        }
        
        public bool CanEditWish(CalendarEventWish wish)
        {
            return wish.AuthorId == User.Id;
        }

        public bool CanEditMessage(CommunicationChannelMessage message)
        {
            return message.UserId == User.Id;
        }
    }
}

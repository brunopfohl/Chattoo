using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Domain.Services
{
    public class ChannelManager
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICalendarEventRepository _calendarEventRepository;
        
        public ChannelManager(IUserRepository userRepository, ICommunicationChannelRepository communicationChannelRepository, ICurrentUserService currentUserService, ICalendarEventRepository calendarEventRepository)
        {
            _userRepository = userRepository;
            _communicationChannelRepository = communicationChannelRepository;
            _currentUserService = currentUserService;
            _calendarEventRepository = calendarEventRepository;
        }

        public async Task<CommunicationChannel> GetChannelOrThrow(string channelId)
        {
            var channel = await _communicationChannelRepository.GetByIdAsync(channelId)
                ?? throw new ChannelNotFoundException(channelId);

            if (!_currentUserService.CanViewChannel(channel))
            {
                throw new ForbiddenAccessException();
            }

            return channel;
        }

        public CommunicationChannel Create(string name, string description)
        {
            var channel = CommunicationChannel.Create(name, description);
            
            channel.AddParticipant(_currentUserService.User.Id);

            return channel;
        }

        #region  Messages
        
        public CommunicationChannelMessage GetMessageOrThrow(CommunicationChannel channel, string messageId)
        {
            var message = channel.GetMessage(messageId)
                ?? throw new MessageNotFoundException(messageId);

            return message;
        }
        
        public CommunicationChannelMessage DeleteMessage(CommunicationChannel channel, string messageId)
        {
            var message = GetMessageOrThrow(channel, messageId);

            if (!_currentUserService.CanEditMessage(message))
            {
                throw new ForbiddenAccessException();
            }

            channel.DeleteMessage(message);

            return message;
        }
        
        public CommunicationChannelMessage UpdateMessage(CommunicationChannel channel, string messageId, string content)
        {
            var message = GetMessageOrThrow(channel, messageId);

            if (!_currentUserService.CanEditMessage(message))
            {
                throw new ForbiddenAccessException();
            }

            message.SetContent(content);

            return message;
        }

        #endregion

        #region Roles

        public CommunicationChannelRole GetRoleOrThrow(CommunicationChannel channel, string roleId)
        {
            return channel.GetRole(roleId)
                ?? throw new ChannelRoleNotFound(channel.Id, roleId);
        }

        #endregion

        public IQueryable<CalendarEvent> GetEvents(CommunicationChannel channel)
        {
            return _calendarEventRepository.GetByCommunicationChannelId(channel.Id);
        }

        public async Task AddParticipantToChannel(CommunicationChannel channel, string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new UserNotFoundException(userId);
            
            channel.AddParticipant(user.Id);
        }
    }
}
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Application.Common.Services
{
    public class ChannelValidationService
    {
        private readonly ICurrentUserService _currentUserService;

        public ChannelValidationService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public void ThrowIfReadPermissionsDenied(CommunicationChannel channel)
        {
            if (!ReadPermissionGranted(channel))
            {
                throw new ForbiddenAccessException();
            }
        }
        
        public bool ReadPermissionGranted(CommunicationChannel channel)
        {
            return _currentUserService.CanViewChannel(channel);
        }

        public void ThrowIfMessageWritePermissionDenied(CommunicationChannelMessage message)
        {
            if (!MessageWritePermissionGranted(message))
            {
                throw new ForbiddenAccessException();
            }
        }
        
        public bool MessageWritePermissionGranted(CommunicationChannelMessage message)
        {
            return _currentUserService.CanEditMessage(message);
        }
    }
}
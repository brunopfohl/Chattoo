using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Services
{
    public class MessageManager
    {
        private readonly ICurrentUserService _currentUserService;
        
        public MessageManager(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public CommunicationChannelMessageAttachment AddAttachment(CommunicationChannelMessage message, string name,
            byte[] content, CommunicationChannelMessageAttachmentType type)
        {
            if (!_currentUserService.CanEditMessage(message))
            {
                throw new ForbiddenAccessException();
            }
            
           return message.AddAttachment(name, content, type);
        }
        
        public CommunicationChannelMessageAttachment DeleteAttachment(CommunicationChannelMessage message, string attachmentId)
        {
            if (!_currentUserService.CanEditMessage(message))
            {
                throw new ForbiddenAccessException();
            }
            
            return message.DeleteAttachment(attachmentId);
        }

        public CommunicationChannelMessageAttachment UpdateAttachment(CommunicationChannelMessage message,
            string attachmentId, string name)
        {
            if (!_currentUserService.CanEditMessage(message))
            {
                throw new ForbiddenAccessException();
            }

            return message.UpdateAttachment(attachmentId, name);
        }
    }
}
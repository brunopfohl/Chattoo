using System;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicitUserInChannelException : Exception
    {
        public DuplicitUserInChannelException(string userId, string channelId)
        {
            UserId = userId;
            ChannelId = channelId;
        }

        public string UserId { get; }
        
        public string ChannelId { get; }
    }
}
using System;

namespace Chattoo.Domain.Exceptions
{
    public class ChannelNotFoundException : Exception
    {
        public ChannelNotFoundException(string channelId)
        {
            ChannelId = channelId;
        }

        public string ChannelId { get; }
    }
}
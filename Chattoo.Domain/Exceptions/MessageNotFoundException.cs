using System;

namespace Chattoo.Domain.Exceptions
{
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException(string messageId)
        {
            MessageId = messageId;
        }

        public string MessageId { get; }
    }
}
using System;

namespace Chattoo.Domain.Exceptions
{
    public class AttachmentNotFoundException : Exception
    {
        public AttachmentNotFoundException(string attachmentId)
        {
            AttachmentId = attachmentId;
        }

        public string AttachmentId { get; }
    }
}
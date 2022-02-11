using System;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicitUserInGroupException : Exception
    {
        public DuplicitUserInGroupException(string groupId, string userId)
        {
            GroupId = groupId;
            UserId = userId;
        }

        public string GroupId { get; }
        
        public string UserId { get; }
    }
}
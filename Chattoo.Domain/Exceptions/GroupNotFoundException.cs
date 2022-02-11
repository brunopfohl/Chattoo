using System;

namespace Chattoo.Domain.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException(string groupId)
        {
            GroupId = groupId;
        }

        public string GroupId { get; }
    }
}
using System;

namespace Chattoo.Domain.Exceptions
{
    public class GroupRoleNotFoundException : Exception
    {
        public GroupRoleNotFoundException(string groupId, string groupRole)
        {
            GroupId = groupId;
            GroupRole = groupRole;
        }

        public string GroupId { get; }
        
        public string GroupRole { get; }
    }
}
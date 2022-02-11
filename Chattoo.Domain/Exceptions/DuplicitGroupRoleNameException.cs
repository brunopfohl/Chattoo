using System;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicitGroupRoleNameException : Exception
    {
        public DuplicitGroupRoleNameException(string groupId, string roleName)
        {
            GroupId = groupId;
            RoleName = roleName;
        }

        public string GroupId { get; }
        
        public string RoleName { get; }
    }
}
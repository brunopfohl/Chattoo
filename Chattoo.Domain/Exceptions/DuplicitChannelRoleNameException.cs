using System;

namespace Chattoo.Domain.Exceptions
{
    public class DuplicitChannelRoleNameException : Exception
    {
        public DuplicitChannelRoleNameException(string channelId, string roleName)
        {
            ChannelId = channelId;
            RoleName = roleName;
        }

        public string ChannelId { get; }
        
        public string RoleName { get; }
    }
}
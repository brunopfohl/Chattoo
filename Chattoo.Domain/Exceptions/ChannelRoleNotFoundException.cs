using System;

namespace Chattoo.Domain.Exceptions
{
    public class ChannelRoleNotFoundException : Exception
    {
        public ChannelRoleNotFoundException(string channelId, string roleId)
        {
            ChannelId = channelId;
            RoleId = roleId;
        }

        public string ChannelId { get; }
        
        public string RoleId { get; }
    }
}
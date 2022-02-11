using System;

namespace Chattoo.Domain.Exceptions
{
    public class ChannelRoleNotFound : Exception
    {
        public ChannelRoleNotFound(string channelId, string roleId)
        {
            ChannelId = channelId;
            RoleId = roleId;
        }

        public string ChannelId { get; }
        
        public string RoleId { get; }
    }
}
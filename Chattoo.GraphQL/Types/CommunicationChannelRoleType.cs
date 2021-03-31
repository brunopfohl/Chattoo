using Chattoo.Application.CommunicationChannelRoles.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelRoleType : ObjectGraphType<CommunicationChannelRoleDto>
    {
        public CommunicationChannelRoleType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Permission);
            Field(o => o.ChannelId);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt);
        }
    }
}
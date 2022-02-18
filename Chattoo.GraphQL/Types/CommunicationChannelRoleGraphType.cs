using Chattoo.Application.Common.DTOs;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelRoleGraphType : AuditableObjectGraphType<CommunicationChannelRoleDto>
    {
        public CommunicationChannelRoleGraphType()
        {
            Name = "CommunicationChannelRole";
            
            Field(o => o.Name);
            // TODO: graphQL neumí EnumerationGraphType<flags enum>, takže musím vytvořit vlastní typ
            //Field(o => o.Permission);
            Field(o => o.ChannelId);
        }
    }
}
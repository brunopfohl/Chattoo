using Chattoo.Application.CommunicationChannels.DTOs;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelGraphType : AuditableObjectGraphType<CommunicationChannelDto>
    {
        public CommunicationChannelGraphType()
        {
            Name = "CommunicationChannel";
            
            Field(o => o.Name);
            Field(o => o.Description);
        }
    }
}
using Chattoo.Application.CommunicationChannels.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelType : ObjectGraphType<CommunicationChannelDto>
    {
        public CommunicationChannelType()
        {
            Name = "CommunicationChannel";
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Description);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt);
        }
    }
}
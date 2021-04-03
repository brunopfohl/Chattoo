using Chattoo.Application.CommunicationChannelMessages.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelMessageType : ObjectGraphType<CommunicationChannelMessageDto>
    {
        public CommunicationChannelMessageType()
        {
            Field(o => o.Id);
            Field(o => o.Content);
            Field<CommunicationChannelMessageTypeType>(nameof(CommunicationChannelMessageDto.Type));
            Field(o => o.ChannelId);
            Field(o => o.UserId);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
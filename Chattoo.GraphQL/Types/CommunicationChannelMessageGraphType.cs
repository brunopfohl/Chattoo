using Chattoo.Application.CommunicationChannelMessages.DTOs;
using Chattoo.GraphQL.Types.Enums;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelMessageGraphType : ObjectGraphType<CommunicationChannelMessageDto>
    {
        public CommunicationChannelMessageGraphType()
        {
            Name = "CommunicationChannelMessage";
        
            Field(o => o.Id);
            Field(o => o.Content);
            Field(o => o.UserName);
            Field<CommunicationChannelMessageTypeGraphType>(nameof(CommunicationChannelMessageDto.Type));
            Field(o => o.ChannelId);
            Field(o => o.UserId);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
using Chattoo.Application.Common.DTOs;
using Chattoo.GraphQL.Types.Enums;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelMessageGraphType : AuditableObjectGraphType<CommunicationChannelMessageDto>
    {
        public CommunicationChannelMessageGraphType()
        {
            Name = "CommunicationChannelMessage";
        
            Field(o => o.Content);
            Field(o => o.UserName);
            Field<CommunicationChannelMessageTypeGraphType>(nameof(CommunicationChannelMessageDto.Type));
            Field(o => o.ChannelId);
            Field(o => o.UserId);
        }
    }
}
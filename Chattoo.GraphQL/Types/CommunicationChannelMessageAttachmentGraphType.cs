using Chattoo.Application.Common.DTOs;
using Chattoo.GraphQL.Types.Enums;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelMessageAttachmentGraphType : ObjectGraphType<CommunicationChannelMessageAttachmentDto>
    {
        public CommunicationChannelMessageAttachmentGraphType()
        {
            Name = "CommunicationChannelMessageAttachment";
            
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Content);
            Field<CommunicationChannelMessageAttachmentTypeGraphType>(nameof(CommunicationChannelMessageAttachmentDto.Type));
            Field(o => o.MessageId);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
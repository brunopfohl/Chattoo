using Chattoo.Application.Common.DTOs;
using Chattoo.GraphQL.Types.Enums;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelMessageAttachmentGraphType : AuditableObjectGraphType<CommunicationChannelMessageAttachmentDto>
    {
        public CommunicationChannelMessageAttachmentGraphType()
        {
            Name = "CommunicationChannelMessageAttachment";
            
            Field(o => o.Name);
            Field(o => o.Content);
            Field<CommunicationChannelMessageAttachmentTypeGraphType>(nameof(CommunicationChannelMessageAttachmentDto.Type));
            Field(o => o.MessageId);
        }
    }
}
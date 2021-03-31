using Chattoo.Application.CommunicationChannelMessageAttachments.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class CommunicationChannelMessageAttachmentType : ObjectGraphType<CommunicationChannelMessageAttachmentDto>
    {
        public CommunicationChannelMessageAttachmentType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Content);
            Field(o => o.Type);
            Field(o => o.MessageId);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt);
        }
    }
}
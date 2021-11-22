using GraphQL.Types;

namespace Chattoo.GraphQL.Types.Enums
{
    public class CommunicationChannelMessageAttachmentTypeGraphType : EnumerationGraphType<Chattoo.Domain.Enums.CommunicationChannelMessageAttachmentType>
    {
        public CommunicationChannelMessageAttachmentTypeGraphType()
        {
            Name = "CommunicationChannelMessageAttachmentType";
        }
    }
}
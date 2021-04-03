using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class CommunicationChannelMessageAttachmentTypeType : EnumerationGraphType<Chattoo.Domain.Enums.CommunicationChannelMessageAttachmentType>
    {
        public CommunicationChannelMessageAttachmentTypeType()
        {
            Name = "bpdf";
        }
    }
}
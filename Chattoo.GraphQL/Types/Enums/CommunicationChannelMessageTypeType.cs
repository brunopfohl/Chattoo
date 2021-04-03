using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class CommunicationChannelMessageTypeType : EnumerationGraphType<Chattoo.Domain.Enums.CommunicationChannelMessageType>
    {
        public CommunicationChannelMessageTypeType()
        {
            Name = "asdf";
        }
    }
}
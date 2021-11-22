using GraphQL.Types;

namespace Chattoo.GraphQL.Types.Enums
{
    public class CommunicationChannelMessageTypeGraphType : EnumerationGraphType<Chattoo.Domain.Enums.CommunicationChannelMessageType>
    {
        public CommunicationChannelMessageTypeGraphType()
        {
            Name = "CommunicationChannelMessageType";
        }
    }
}
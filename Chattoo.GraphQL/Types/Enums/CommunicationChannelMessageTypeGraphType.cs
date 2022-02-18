using Chattoo.Domain.Enums;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types.Enums
{
    public class CommunicationChannelMessageTypeGraphType : EnumerationGraphType<CommunicationChannelMessageType>
    {
        public CommunicationChannelMessageTypeGraphType()
        {
            Name = "CommunicationChannelMessageType";
        }
    }
}
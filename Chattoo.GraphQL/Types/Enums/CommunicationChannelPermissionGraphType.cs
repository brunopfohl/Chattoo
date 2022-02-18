using Chattoo.Domain.Enums;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types.Enums
{
    // NOTE: Kvůli flags enum toto pravděpodobně nebude fungovat.
    public class CommunicationChannelPermissionGraphType : EnumerationGraphType<CommunicationChannelPermission>
    {
        public CommunicationChannelPermissionGraphType()
        {
            Name = "CommunicationChannelPermission";
        }
    }
}
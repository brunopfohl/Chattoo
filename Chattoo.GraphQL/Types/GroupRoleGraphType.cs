using Chattoo.Application.Common.DTOs;

namespace Chattoo.GraphQL.Types
{
    public sealed class GroupRoleGraphType : AuditableObjectGraphType<GroupRoleDto>
    {
        public GroupRoleGraphType()
        {
            Name = "GroupRole";
            
            Field(o => o.Name);
            // TODO: graphQL neumí EnumerationGraphType<flags enum>, takže musím vytvořit vlastní typ
            //Field(o => o.Permission);
        }
    }
}
using Chattoo.Application.GroupRoles.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class GroupRoleType : ObjectGraphType<GroupRoleDto>
    {
        public GroupRoleType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            // TODO: graphQL neumí EnumerationGraphType<flags enum>, takže musím vytvořit vlastní typ
            //Field(o => o.Permission);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
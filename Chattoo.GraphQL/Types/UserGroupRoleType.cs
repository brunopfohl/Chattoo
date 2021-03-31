using Chattoo.Application.GroupRoles.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class UserGroupRoleType : ObjectGraphType<GroupRoleDto>
    {
        public UserGroupRoleType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Permission);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt);
        }
    }
}
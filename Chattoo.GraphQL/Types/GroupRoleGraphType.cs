using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Domain.Entities;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class GroupRoleGraphType : ObjectGraphType<GroupRoleDto>
    {
        public GroupRoleGraphType()
        {
            Name = "GroupRole";
            
            Field(o => o.Id);
            Field(o => o.Name);
            // TODO: graphQL neumí EnumerationGraphType<flags enum>, takže musím vytvořit vlastní typ
            //Field(o => o.Permission);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
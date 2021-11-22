using Chattoo.Application.Groups.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class GroupGraphType : ObjectGraphType<GroupDto>
    {
        public GroupGraphType()
        {
            Name = "Group";
            
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
using Chattoo.Application.Groups.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public sealed class GroupType : ObjectGraphType<GroupDto>
    {
        public GroupType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
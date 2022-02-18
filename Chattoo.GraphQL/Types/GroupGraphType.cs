using Chattoo.Application.Groups.DTOs;

namespace Chattoo.GraphQL.Types
{
    public sealed class GroupGraphType : AuditableObjectGraphType<GroupDto>
    {
        public GroupGraphType()
        {
            Name = "Group";
            
            Field(o => o.Name);
        }
    }
}
using Chattoo.Application.Common.DTOs;

namespace Chattoo.GraphQL.Types
{
    public class UserAliasGraphType : AuditableObjectGraphType<UserAliasDto>
    {
        public UserAliasGraphType()
        {
            Name = "UserAlias";
            
            Field(o => o.UserId);
            Field(o => o.Alias);
        }
    }
}
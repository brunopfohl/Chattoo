using Chattoo.Application.Common.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class UserAliasGraphType : ObjectGraphType<UserAliasDto>
    {
        public UserAliasGraphType()
        {
            Name = "UserAlias";
            
            Field(o => o.Id);
            Field(o => o.UserId);
            Field(o => o.Alias);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
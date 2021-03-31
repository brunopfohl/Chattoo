using Chattoo.Application.UserAliases.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class UserAliasType : ObjectGraphType<UserAliasDto>
    {
        public UserAliasType()
        {
            Field(o => o.Id);
            Field(o => o.UserId);
            Field(o => o.Alias);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt);
        }
    }
}
using Chattoo.Application.Users.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class UserGraphType : ObjectGraphType<UserDto>
    {
        public UserGraphType()
        {
            Name = "User";
            
            Field(o => o.Id);
            Field(o => o.UserName);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
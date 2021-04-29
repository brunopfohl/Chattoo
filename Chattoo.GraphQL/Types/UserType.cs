using Chattoo.Application.Users.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class UserType : ObjectGraphType<UserDto>
    {
        public UserType()
        {
            Field(o => o.Id);
            Field(o => o.UserName);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, nullable: true);
        }
    }
}
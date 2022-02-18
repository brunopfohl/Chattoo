using Chattoo.Application.Users.DTOs;

namespace Chattoo.GraphQL.Types
{
    public class UserGraphType : AuditableObjectGraphType<UserDto>
    {
        public UserGraphType()
        {
            Name = "User";
            
            Field(o => o.UserName);
        }
    }
}
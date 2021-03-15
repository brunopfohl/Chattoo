using System.Collections.Generic;
using System.Security.Claims;

namespace Chattoo.GraphQL
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public GraphQLUserContext()
        {
            
        }

        public GraphQLUserContext(ClaimsPrincipal user)
        {
            this.User = user;
        }

        public ClaimsPrincipal User { get; set; }
    }
}

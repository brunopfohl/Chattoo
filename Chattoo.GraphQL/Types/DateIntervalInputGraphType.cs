using Chattoo.Application.Common.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class DateIntervalInputGraphType : InputObjectGraphType<DateIntervalDto>
    {
        public DateIntervalInputGraphType()
        {
            Field(x => x.StartsAt);
            Field(x => x.EndsAt);
        }
    }
}
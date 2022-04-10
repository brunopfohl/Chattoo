using Chattoo.Application.Common.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class DateIntervalGraphType : ObjectGraphType<DateIntervalDto>
    {
        public DateIntervalGraphType()
        {
            Field(o => o.Id);
            Field(o => o.StartsAt);
            Field(o => o.EndsAt);
        }
    }
}
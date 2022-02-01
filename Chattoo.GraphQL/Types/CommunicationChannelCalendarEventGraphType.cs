using Chattoo.Application.CalendarEvents.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public class CommunicationChannelCalendarEventGraphType : ObjectGraphType<CalendarEventDto>
    {
        public CommunicationChannelCalendarEventGraphType()
        {
            Name = "CommunicationChannelCalendarEvent";
            
            Field(o => o.Id);
            Field(o => o.StartsAt);
            Field(o => o.EndsAt, true);
            Field(o => o.Name);
            Field(o => o.Description);
            Field(o => o.AuthorId);
            Field(o => o.CreatedAt);
            Field(o => o.ModifiedAt, true);
        }
    }
}
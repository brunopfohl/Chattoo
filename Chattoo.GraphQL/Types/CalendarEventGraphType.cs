using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.GraphQL.Types.Enums;

namespace Chattoo.GraphQL.Types
{
    public class CalendarEventGraphType : AuditableObjectGraphType<CalendarEventDto>
    {
        public CalendarEventGraphType()
        {
            Name = "CalendarEvent";
            
            Field(o => o.StartsAt);
            Field(o => o.EndsAt, true);
            
            Field(o => o.Name);
            Field(o => o.Description);
            Field(o => o.MaximalParticipantsCount, true);
            Field(o => o.ParticipantsCount);
            Field<CalendarEventTypeGraphType>(nameof(CalendarEventDto.Type));
            
            Field(o => o.AuthorId);
            Field(o => o.GroupId, true);
            Field(o => o.CommunicationChannelId, true);
        }
    }
}
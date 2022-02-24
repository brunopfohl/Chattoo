using FluentValidation;

namespace Chattoo.Application.CalendarEvents.Queries
{
    public class GetUsersForCalendarEventQueryValidator : AbstractValidator<GetUsersForCalendarEventQuery>
    {
        public GetUsersForCalendarEventQueryValidator()
        {
            RuleFor(v => v.EventId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id události.");
        }
    }
}
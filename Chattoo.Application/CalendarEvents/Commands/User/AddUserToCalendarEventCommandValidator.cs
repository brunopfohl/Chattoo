using FluentValidation;

namespace Chattoo.Application.CalendarEvents.Commands.User
{
    public class AddUserToCalendarEventCommandValidator : AbstractValidator<AddUserToCalendarEventCommand>
    {
        public AddUserToCalendarEventCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Id uživatele musí být určeno.");
            
            RuleFor(v => v.EventId)
                .NotEmpty().WithMessage("Id události musí být určeno.");
        }
    }
}
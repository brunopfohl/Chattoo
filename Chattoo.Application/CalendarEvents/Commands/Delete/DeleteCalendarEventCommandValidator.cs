using FluentValidation;

namespace Chattoo.Application.CalendarEvents.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCalendarEventCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelCalendarEventCommandValidator : AbstractValidator<DeleteCalendarEventCommand>
    {
        public DeleteCommunicationChannelCalendarEventCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné zadat Id události z komunikačního kanálu.");
        }
    }
}
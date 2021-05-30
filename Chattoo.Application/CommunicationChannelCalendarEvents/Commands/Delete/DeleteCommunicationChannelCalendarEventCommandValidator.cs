using FluentValidation;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCommunicationChannelCalendarEventCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelCalendarEventCommandValidator : AbstractValidator<DeleteCommunicationChannelCalendarEventCommand>
    {
        public DeleteCommunicationChannelCalendarEventCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné zadat Id události z komunikačního kanálu.");
        }
    }
}
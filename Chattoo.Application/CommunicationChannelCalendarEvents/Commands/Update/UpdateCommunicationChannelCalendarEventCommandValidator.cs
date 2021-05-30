using FluentValidation;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Update
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateCommunicationChannelCalendarEventCommand"/>.
    /// </summary>
    public class UpdateCommunicationChannelCalendarEventCommandValidator : AbstractValidator<UpdateCommunicationChannelCalendarEventCommand>
    {
        public UpdateCommunicationChannelCalendarEventCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id kalendářní události musí být specifikováno.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název události v komunikačním kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název události v komunikačního kanálu je nutné vyplnit.");
            
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Popis události z komunikačního kanálu je nutné vyplnit.");
            
            RuleFor(v => v.StartsAt)
                .NotEmpty().WithMessage("Počátek události musí být specifikován.");

            RuleFor(v => v.EndsAt)
                .GreaterThan(v => v.StartsAt).WithMessage("Konec události musí následovat po počátku události");
        }
    }
}
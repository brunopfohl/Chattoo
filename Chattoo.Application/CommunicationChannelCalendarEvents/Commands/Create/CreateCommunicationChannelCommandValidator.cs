using FluentValidation;

namespace Chattoo.Application.CommunicationChannelCalendarEvents.Commands.Create
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateCommunicationChannelCalendarEventCommand"/>.
    /// </summary>
    public class CreateCommunicationChannelCalendarEventCommandValidator : AbstractValidator<CreateCommunicationChannelCalendarEventCommand>
    {
        public CreateCommunicationChannelCalendarEventCommandValidator()
        {
            RuleFor(v => v.CommunicationChannelId)
                .NotEmpty().WithMessage("Id komunikačního kanálu musí být specifikováno.");
            
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
using Chattoo.Domain.Extensions;
using FluentValidation;

namespace Chattoo.Application.CalendarEvents.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateCalendarEventCommand"/>.
    /// </summary>
    public class CreateCalendarEventCommandValidator : AbstractValidator<CreateCalendarEventCommand>
    {
        public CreateCalendarEventCommandValidator()
        {
            RuleFor(v => v.CommunicationChannelId)
                .NotEmpty()
                .When(cmd => cmd.GroupId.IsNotNullOrEmpty())
                    .WithMessage("Id komunikačního kanálu nebo Id skupiny musí být zadáno.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název události v komunikačním kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název události v komunikačního kanálu je nutné vyplnit.");
            
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Popis události z komunikačního kanálu je nutné vyplnit.");
            
            RuleFor(v => v.StartsAt)
                .NotEmpty().WithMessage("Počátek události musí být specifikován.");

            RuleFor(v => v.MaximalParticipantsCount)
                .GreaterThan(0)
                    .WithMessage("Max. počet účastníků musí být neomezený nebo větší než 0");

            RuleFor(v => v.EndsAt)
                .GreaterThan(v => v.StartsAt)
                    .WithMessage("Konec události musí následovat po počátku události");
        }
    }
}
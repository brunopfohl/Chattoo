using FluentValidation;

namespace Chattoo.Application.CommunicationChannelMessages.Commands.Create
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateCommunicationChannelMessageCommand"/>.
    /// </summary>
    public class CreateCommunicationChannelMessageCommandValidator : AbstractValidator<CreateCommunicationChannelMessageCommand>
    {
        public CreateCommunicationChannelMessageCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty().WithMessage("Je nutné vyplnit Id komunikačního kanálu.");
            
            RuleFor(v => v.Content)
                .MaximumLength(1000).WithMessage("Obsah zprávy v komunikačním kanálu nesmí být delší než 1000 znaků.")
                .NotEmpty().WithMessage("Obsah zprávy v komunikačním kanálu je nutné vyplnit.");
        }
    }
}
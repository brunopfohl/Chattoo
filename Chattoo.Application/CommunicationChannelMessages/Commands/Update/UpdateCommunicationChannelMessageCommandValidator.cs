using FluentValidation;

namespace Chattoo.Application.CommunicationChannelMessages.Commands.Update
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateCommunicationChannelMessageCommand"/>.
    /// </summary>
    public class UpdateCommunicationChannelMessageCommandValidator : AbstractValidator<UpdateCommunicationChannelMessageCommand>
    {
        public UpdateCommunicationChannelMessageCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id zprávy z komunikačního kanálu.");
            
            RuleFor(v => v.Content)
                .MaximumLength(1000).WithMessage("Obsah zprávy v komunikačním kanálu nesmí být delší než 1000 znaků.")
                .NotEmpty().WithMessage("Obsah zprávy v komunikačním kanálu je nutné vyplnit.");
        }
    }
}
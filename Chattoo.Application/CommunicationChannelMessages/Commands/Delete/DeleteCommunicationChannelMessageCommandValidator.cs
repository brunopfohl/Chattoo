using FluentValidation;

namespace Chattoo.Application.CommunicationChannelMessages.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCommunicationChannelMessageCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelMessageCommandValidator : AbstractValidator<DeleteCommunicationChannelMessageCommand>
    {
        public DeleteCommunicationChannelMessageCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id zprávy z komunikačního kanálu.");
        }
    }
}
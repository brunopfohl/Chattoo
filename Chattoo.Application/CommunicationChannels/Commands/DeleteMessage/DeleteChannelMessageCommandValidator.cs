using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands.DeleteMessage
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteChannelMessageCommand"/>.
    /// </summary>
    public class DeleteChannelMessageCommandValidator : AbstractValidator<DeleteChannelMessageCommand>
    {
        public DeleteChannelMessageCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id zprávy z komunikačního kanálu.");
        }
    }
}
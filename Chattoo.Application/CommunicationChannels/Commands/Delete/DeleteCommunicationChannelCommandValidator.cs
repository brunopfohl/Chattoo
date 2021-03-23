using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCommunicationChannelCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelCommandValidator : AbstractValidator<DeleteCommunicationChannelCommand>
    {
        public DeleteCommunicationChannelCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné zadat Id komunikačního kanálu.");
        }
    }
}